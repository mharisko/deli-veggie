
namespace DeliVeggie.Product.Service.Mongo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Mongo.Core.Repository;
    using DeliVeggie.Product.Service.Mongo.Mdo;
    using DeliVeggie.Product.Service.Dto;
    using MongoDB.Driver;
    using DeliVeggie.Product.Service.Abstract.Repository;
    using MongoDB.Bson.Serialization;

    public class ProductRepository : MongoRepository<ProductMdo, string>, IProductRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="connectionString">Connectionstring to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public ProductRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(ProductDto)))
            {
                BsonClassMap.RegisterClassMap<ProductDto>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(x => x.Id);
                    cm.MapProperty(x => x.EntryDate)
                      .SetElementName("CreatedDate");
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        /// <summary>
        /// Adds the new product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        public async Task AddNewProductAsync(ProductDto product)
        {
            var mdo = new ProductMdo
            {
                Name = product.Name,
                Price = product.Price,
                CreatedDate = DateTime.Now,
            };

            await this.Collection.InsertOneAsync(mdo);
        }

        /// <summary>
        /// Updates the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="product">The product.</param>
        public async Task<long> UpdateProductAsync(string productId, ProductDto product)
        {
            var filter = Builders<ProductMdo>.Filter
                        .Eq(x => x.Id, productId);

            var update = Builders<ProductMdo>.Update
                            .Set(x => x.Name, product.Name)
                            .Set(x => x.Price, product.Price)
                            .Set(x => x.UpdatedDate, DateTime.Now);

            var updateResult = await this.Collection.UpdateOneAsync(filter, update);
            return updateResult.ModifiedCount;
        }

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        public async Task<long> DeleteProductAsync(string productId)
        {
            var filter = Builders<ProductMdo>.Filter
                      .Eq(x => x.Id, productId);

            var deleteResult = await this.Collection.DeleteOneAsync(filter);
            return deleteResult.DeletedCount;
        }

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Get product details by product id</returns>
        public async Task<ProductDto> GetProductAsync(string productId)
        {
            var filter = Builders<ProductMdo>.Filter
                     .Eq(x => x.Id, productId);
            var options = new FindOptions<ProductMdo, ProductDto>
            {
                Projection = Builders<ProductMdo>.Projection.Expression(x => new ProductDto
                {
                    Id = x.Id,
                    EntryDate = x.CreatedDate,
                    Name = x.Name,
                    Price = x.Price
                })
            };

            var documents = await this.Collection.FindAsync(filter, options);
            return await documents.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the products asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int skip, int limit)
        {
            var options = new FindOptions<ProductMdo, ProductDto>
            {
                Skip = skip,
                Limit = limit,
                Projection = Builders<ProductMdo>.Projection.Expression(x => new ProductDto
                {
                    Id = x.Id,
                    EntryDate = x.CreatedDate,
                    Name = x.Name,
                    Price = x.Price
                }),
                Sort = Builders<ProductMdo>.Sort.Ascending(s => s.CreatedDate)
            };

            var documents = await this.Collection.FindAsync(Builders<ProductMdo>.Filter.Empty, options);
            return await documents.ToListAsync();
        }

        /// <summary>
        /// Gets the count asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetCountAsync()
        {
            return await this.Collection.EstimatedDocumentCountAsync();
        }
    }
}
