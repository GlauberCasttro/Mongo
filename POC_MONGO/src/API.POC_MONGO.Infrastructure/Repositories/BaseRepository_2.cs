using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Infrastructure.Mongo;
using MongoDB.Driver;

namespace API.POC_MONGO.Infrastructure.Repositories
{
    public class BaseRepository_2<TEntity> : IRepository_2<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository_2(IMongoContext context, string collectionName)
        {
            Context = context;

            DbSet = Context.GetCollection<TEntity>(collectionName);
        }

        public void Dispose() => Context?.Dispose();
    }
}