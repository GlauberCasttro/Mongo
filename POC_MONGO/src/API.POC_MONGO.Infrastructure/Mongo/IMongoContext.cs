using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);

        Task<int> SaveChanges();

        IMongoCollection<T> GetCollection<T>(string name);

        Task<IClientSessionHandle> InitTransaction();
        Task Commit();
    }
}
