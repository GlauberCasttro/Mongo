using MongoDB.Driver;

namespace Infra.Repositories
{
    public interface ICollectionMongoFactory<T>
    {
        IMongoCollection<T> Collection(string collection);
    }
}