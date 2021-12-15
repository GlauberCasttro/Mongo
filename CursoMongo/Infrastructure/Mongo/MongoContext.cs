using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Mongo
{
    /// <summary>
    /// Classe que conecta ao mongo
    /// </summary>
    public class MongoDbContext
    {
        public IMongoDatabase Db { get; private set; }

        public MongoDbContext(IOptions<MongoSettings> config)
        {
            mongoConfig = config.Value;
            Configure();
        }

        private void Configure()
        {
            try
            {
                var client = new MongoClient(mongoConfig.ConnectionString);
                Db = client.GetDatabase(mongoConfig.Db);

                Mapping.MapearMongo();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Ocorreu um erro ao realizar a conexão com o mongo", ex);
                throw new MongoException("Ocorreu um erro ao conectar ao mongo", ex);
            }
        }
    }
}
