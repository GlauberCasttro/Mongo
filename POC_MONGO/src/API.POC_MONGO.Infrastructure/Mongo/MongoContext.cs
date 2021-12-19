using API.POC_MONGO.Infrastructure.Mongo.Mappings;
using MongoDB.Driver;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoClient;

        public MongoContext(IMongoClient mongoClient)
        {
            (_mongoClient) = (mongoClient);

            MappingSchema.MapearEntidadesMongo();
        }

        public IMongoCollection<T> GetCollection<T>(string name) => GetDatabase().GetCollection<T>(name);

        private IMongoDatabase GetDatabase() => _mongoClient.GetDatabase(Config.Mongo.Database);
    }
}