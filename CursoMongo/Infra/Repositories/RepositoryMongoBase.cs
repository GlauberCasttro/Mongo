using Infra.Data.Mongo;
using Infra.Data.Schemas;
using MongoDB.Driver;

namespace Infra.Repositories
{
    /// <summary>
    /// Classe de configuração de repositorio generico do mongo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryMongoBase<T> where T : ISchemaMongoBase
    {
        public IMongoCollection<T> _collection;

        public RepositoryMongoBase(MongoDbContext mongo, string CollectionName
            )
        {
            _collection = mongo.GetDataBase().GetCollection<T>(CollectionName);
        }
    }
}