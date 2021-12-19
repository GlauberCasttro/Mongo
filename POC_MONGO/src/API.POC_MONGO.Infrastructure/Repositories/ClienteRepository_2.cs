using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure.Mongo;
using API.POC_MONGO.Infrastructure.Schemas;
using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Repositories
{
    public class ClienteRepository_2 : BaseRepository_2<ClienteSchema>, IClienteRepository_2
    {
        private readonly IMapper _mapper;

        public ClienteRepository_2(IMapper mapper, IMongoContext context)
            : base(context, "clientes")
        {
            _mapper = mapper;
        }

        public async Task Adicionar(Cliente cliente)
        {
            var document = _mapper.Map<ClienteSchema>(cliente);

            await DbSet.InsertOneAsync(document);
        }

        public async Task AtualizarCompleto(Cliente restaurante)
        {
            var document = _mapper.Map<ClienteSchema>(restaurante);
            await Task.Run(() => DbSet.ReplaceOneAsync(e => e.Id == document.Id, document));
        }

        public async Task AtualizarSituacao(string id, Situacao situacao)
        {
            var atualizacao = Builders<ClienteSchema>.Update.Set(e => e.Situacao, situacao);
            var result = await DbSet.UpdateOneAsync(e => e.Id == id, atualizacao);
        }

        public async Task<Cliente> ObterPorId_2(string id)
        {
            var document = await Task.Run(() => DbSet.AsQueryable()
                            .FirstOrDefault(e => e.Id == id)
                           );

            return _mapper.Map<Cliente>(document);
        }

        public async Task<Cliente> ObterPorId(string id)
        {
            var document = await Task.Run(() => (from e in DbSet.AsQueryable()
                                                 where e.Nome == id
                                                 select new ClienteSchema
                                                 {
                                                     Id = e.Id,
                                                     Nome = e.Nome,
                                                     Cpf = e.Cpf,
                                                 }).FirstOrDefault());

            return _mapper.Map<Cliente>(document);
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            var documents = new List<ClienteSchema>();

            await DbSet.AsQueryable().ForEachAsync(e =>
            {
                documents.Add(e);
            });

            return _mapper.Map<IEnumerable<Cliente>>(documents);
        }
    }
}