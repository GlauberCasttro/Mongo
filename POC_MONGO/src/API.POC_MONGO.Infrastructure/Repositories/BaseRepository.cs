using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Infrastructure.Mongo;
using API.POC_MONGO.Infrastructure.Schemas;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TSchema> : IRepository<TEntity>
        where TEntity : class
        where TSchema : ISchemaRoot
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TSchema> DbSet;
        private readonly IMapper _mapper;
        private readonly IClientSessionHandle _clientSessionHandle;

        protected BaseRepository(IMongoContext context, IMapper mapper, IClientSessionHandle clientSessionHandle, string collectionName)
        {
            Context = context;
            DbSet = Context.GetCollection<TSchema>(collectionName);
            _clientSessionHandle = clientSessionHandle;
            _mapper = mapper;
        }

        public virtual async Task Adicionar(TEntity obj)
        {
            var dataSchema = _mapper.Map<TSchema>(obj);
            await Task.Run(() => DbSet.InsertOneAsync(_clientSessionHandle, dataSchema));
        }

        public virtual async Task<TEntity> ObterPorId(string id)
        {
            var command = Builders<TSchema>.Filter.Eq("_id", ObjectId.Parse(id));
            var dataSchema = await DbSet.FindAsync(command);

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
            var command = Builders<TSchema>.Filter.Eq("_id", ObjectId.Parse(obj.GetId().ToString()));
            await Task.Run(() => DbSet.ReplaceOneAsync(_clientSessionHandle, command, dataSchema));
        }

        public virtual async Task Remover(string id)
        {
            var command = Builders<TSchema>.Filter.Eq("_id", ObjectId.Parse(id));
            await Task.Run(() => DbSet.DeleteOneAsync(_clientSessionHandle, command));
        }

        public void Dispose() => _clientSessionHandle?.Dispose();
    }
}