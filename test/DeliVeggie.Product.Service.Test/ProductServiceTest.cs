using System;
using System.Threading.Tasks;
using DeliVeggie.Product.Service.Abstract.Repository;
using DeliVeggie.Product.Service.Domain;
using DeliVeggie.Product.Service.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DeliVeggie.Product.Service.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        private ProductService productService;
        private PriceReductionService priceReductionService;
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IPriceReductionRepository> priceReductionRepositoryMock;

        [TestInitialize]
        public void Init()
        {
            this.productRepositoryMock = new Mock<IProductRepository>();
            this.priceReductionRepositoryMock = new Mock<IPriceReductionRepository>();
            this.priceReductionService = new PriceReductionService(this.priceReductionRepositoryMock.Object);
            this.productService = new ProductService(this.productRepositoryMock.Object, this.priceReductionService);
        }

        [TestMethod]
        public async Task Add_Product_With_Success()
        {
            var product = new ProductDto { Id = "21ABE7E3-8AAA-4228-9594-5B688AA84BAA", Name = "Uncle Ben's Rice", Price = 25 };
            this.productRepositoryMock.Setup(x => x.AddNewProductAsync(product)).Returns(Task.CompletedTask);

            await this.productService.AddNewProductAsync(product);

            this.productRepositoryMock.Verify(x => x.AddNewProductAsync(product), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Add_Product_With_Error()
        {
            this.productRepositoryMock.Setup(x => x.AddNewProductAsync(It.IsAny<ProductDto>())).Throws(new Exception());
            var product = new ProductDto();

            await this.productService.AddNewProductAsync(product);
        }

        [TestMethod]
        public async Task Get_Product_With_Price_Success()
        {
            this.priceReductionRepositoryMock.Setup(x => x.GetPriceReductionAsync(It.IsAny<int>()))
                .ReturnsAsync(new PriceReductionDto { DayOfWeek = 1, Reduction = .25 });

            var product = new ProductDto { Id = "21ABE7E3-8AAA-4228-9594-5B688AA84BAA", Name = "Uncle Ben's Rice", Price = 25 };
            this.productRepositoryMock.Setup(x => x.GetProductAsync(It.IsAny<string>()))
                .ReturnsAsync(product);

            var withPrice = await this.productService.GetProductWithPriceAsync("21ABE7E3-8AAA-4228-9594-5B688AA84BAA", 1);

            this.priceReductionRepositoryMock.Verify(x => x.GetPriceReductionAsync(It.IsAny<int>()), Times.Once());

            Assert.IsTrue(withPrice.Id == product.Id);
        }
    }
}
