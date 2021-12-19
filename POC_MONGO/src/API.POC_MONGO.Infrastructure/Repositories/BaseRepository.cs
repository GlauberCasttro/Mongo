using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Infrastructure.Mongo;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TSchema> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TSchema> DbSet;
        private readonly IMapper _mapper;
        private readonly IClientSessionHandle clientSessionHandle;

        protected BaseRepository(IMongoContext context, string collectionName, IMapper mapper, IClientSessionHandle clientSessionHandle)
        {
            Context = context;

            DbSet = Context.GetCollection<TSchema>(collectionName);
            _mapper = mapper;
            this.clientSessionHandle = clientSessionHandle;
        }

        public virtual async Task Adicionar(TEntity obj)
        {
            var dataSchema = _mapper.Map<TSchema>(obj);
            await Task.Run(() => DbSet.InsertOneAsync(dataSchema));
        }

        public virtual async Task<TEntity> ObterPorId(string id)
        {
            var dataSchema = await DbSet.FindAsync(Builders<TSchema>.Filter.Eq("_id", ObjectId.Parse(id)));

            return _mapper.Map<TEntity>(dataSchema.FirstOrDefault());
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            var dataSchema = await DbSet.FindAsync(Builders<TSchema>.Filter.Empty);

            return _mapper.Map<IEnumerable<TEntity>>(dataSchema.ToList());
        }

        public virtual async Task Atualizar(TEntity obj)
        {
            var dataSchema = _mapper.Map<TSchema>(obj);
            await Task.Run(() => Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TSchema>.Filter.Eq("_id", ObjectId.Parse(obj.GetId().ToString())), dataSchema)));
        }

        public virtual async Task Remover(string id)
        {
            await Task.Run(() => Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TSchema>.Filter.Eq("_id", ObjectId.Parse(id)))));
        }

        public void Dispose() => Context?.Dispose();
    }
}