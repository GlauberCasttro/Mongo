using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
