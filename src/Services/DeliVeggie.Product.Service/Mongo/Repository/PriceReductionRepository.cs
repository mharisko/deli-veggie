
namespace DeliVeggie.Product.Service.Mongo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Abstract.Repository;
    using DeliVeggie.Product.Service.Dto;
    using DeliVeggie.Product.Service.Mongo.Core.Repository;
    using DeliVeggie.Product.Service.Mongo.Mdo;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;

    public class PriceReductionRepository : MongoRepository<PriceReductionMdo, string>, IPriceReductionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriceReductionRepository"/> class.
        /// </summary>
        /// <param name="connectionString">Connectionstring to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public PriceReductionRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PriceReductionDto)))
            {
                BsonClassMap.RegisterClassMap<PriceReductionDto>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        /// <summary>
        /// Adds the price reduction asynchronous.
        /// </summary>
        /// <param name="priceReduction">The price reduction.</param>
        public async Task AddPriceReductionAsync(PriceReductionDto priceReduction)
        {
            var filter = Builders<PriceReductionMdo>.Filter
                      .Eq(x => x.DayOfWeek, priceReduction.DayOfWeek);

            var update = Builders<PriceReductionMdo>.Update
                            .Set(x => x.DayOfWeek, priceReduction.DayOfWeek)
                            .Set(x => x.Reduction, priceReduction.Reduction)
                            .Set(x => x.CreatedDate, DateTime.Now);

            await this.Collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// Updates the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="priceReduction">The price reduction.</param>
        public async Task UpdatePriceReductionAsync(int dayOfWeek, PriceReductionDto priceReduction)
        {
            var filter = Builders<PriceReductionMdo>.Filter
                       .Eq(x => x.DayOfWeek, dayOfWeek);

            var update = Builders<PriceReductionMdo>.Update
                            .Set(x => x.Reduction, priceReduction.Reduction)
                            .Set(x => x.UpdatedDate, DateTime.Now);

            await this.Collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Deletes the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        public async Task DeletePriceReductionAsync(int dayOfWeek)
        {
            var filter = Builders<PriceReductionMdo>.Filter
                     .Eq(x => x.DayOfWeek, dayOfWeek);

            await this.Collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Gets the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public async Task<PriceReductionDto> GetPriceReductionAsync(int dayOfWeek)
        {
            var filter = Builders<PriceReductionMdo>.Filter
                    .Eq(x => x.DayOfWeek, dayOfWeek);
            var options = new FindOptions<PriceReductionMdo, PriceReductionDto>
            {
                Projection = Builders<PriceReductionMdo>.Projection.As<PriceReductionDto>()
            };

            var documents = await this.Collection.FindAsync(filter, options);
            return await documents.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the price reductions asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PriceReductionDto>> GetPriceReductionsAsync()
        {
            var options = new FindOptions<PriceReductionMdo, PriceReductionDto>
            {
                Projection = Builders<PriceReductionMdo>.Projection.As<PriceReductionDto>()
            };

            var documents = await this.Collection.FindAsync(Builders<PriceReductionMdo>.Filter.Empty, options);
            return await documents.ToListAsync();
        }
    }
}
