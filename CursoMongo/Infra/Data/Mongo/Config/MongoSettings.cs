namespace Infra.Data.Mongo.Config
{
    /// <summary>
    /// Configuraçooes do banco para o mongo
    /// </summary>
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}       