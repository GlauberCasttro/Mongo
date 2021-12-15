using Infra.Data.Mappings;
using Infra.Data.Mongo.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Infra.Data.Mongo
{
    /// <summary>
    /// Classe que conecta ao mongo
    /// </summary>
    public class MongoDbContext
    {
        public IMongoDatabase MongoClient { get; private set; }

        private readonly MongoSettings mongoConfig;
        public MongoDbContext(IOptions<MongoSettings> config)
        {
            mongoConfig = config.Value;
            Configure();
        }

        private void Configure()
        {
            try
            {
                GetDataBase();
                Mapping.MapearEntidadesMongo();
            }
            catch (Exception ex)
            {
                throw new MongoException("Ocorreu um erro ao conectar ao mongo", ex);
            }
        }

        public IMongoDatabase GetDataBase()
        {
            var client = new MongoClient(mongoConfig.ConnectionString);
            return client.GetDatabase(mongoConfig.Database);
        }
    }
}

#region OutroModo
//public MongoDb(IConfiguration configuration, IOptions<MongoConfig> config)
//{
//    try
//    {
//        //var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["connectionString"]));
//        //var client = new MongoClient(settings);
//        //Db = client.GetDatabase(configuration["NomeBanco"]);

//        mongoConfig = config.Value;
//        var client = new MongoClient(mongoConfig.ConnectionString);
//        db = client.GetDatabase(mongoConfig.Db);

//        Mapping.MapearMongo();
//    }
//    catch (Exception ex)
//    {
//        throw
//            new MongoException("Ocorreu um erro ao conectar ao mongo", ex);
//    }
//}

#endregion