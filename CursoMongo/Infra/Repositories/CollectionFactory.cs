using Domain.ValueObject;
using Infra.Data.Mongo;
using Infra.Data.Schemas;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class CollectionFactory<T> : ICollectionMongoFactory<T> where T : ISchemaMongoBase
    {
        private readonly MongoDbContext mongo;

        public CollectionFactory(MongoDbContext mongo)
        {
            this.mongo = mongo;
        }

        public IMongoCollection<T> Collection(string collection)
        {
            return mongo.GetDataBase()
                .GetCollection<T>(collection);
        }
    }


    public class AvaliacaoRepository2 : ICollectionMongoFactory<AvaliacaoSchema>, IAvalicacaoRepository
    {
 
        public IMongoCollection<AvaliacaoSchema> Collection(string collection)
        {
            throw new System.NotImplementedException();
        }

        public Task IncluirAvalicacao(string restauranteId, Avaliacao avalicacao)
        {
            throw new System.NotImplementedException();
        }
    }
}