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
    public class ClienteRepository : BaseRepository<Cliente, ClienteSchema>, IClienteRepository
    {
        private readonly IMapper _mapper;

        public ClienteRepository(IMapper mapper, IMongoContext context, IClientSessionHandle clientSessionHandle)
            : base(context, mapper, clientSessionHandle, "clientes")
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> Obter(string nome, Situacao situacao)
        {
            var builder = Builders<ClienteSchema>.Filter;

            if (!string.IsNullOrEmpty(nome))
            {
                var nameFilter = Builders<ClienteSchema>.Filter.Eq(x => x.Nome, nome);
                builder.And(nameFilter);
            }

            var situacaofilter = Builders<ClienteSchema>.Filter.Eq(x => x.Situacao, situacao);

            var combineFilters = builder.And(situacaofilter);

            var clientes = await Task.Run(() => DbSet.Find(combineFilters).ToList());

            return _mapper.Map<IEnumerable<Cliente>>(clientes);
        }
        public async Task<IEnumerable<Cliente>> Obter(string nome, IEnumerable<Situacao> situacao)
        {
            var builder = Builders<ClienteSchema>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(nome))
            {
                var nameFilter = Builders<ClienteSchema>.Filter.Eq(x => x.Nome, nome);
                filter &= nameFilter;
            }

            if (situacao.Any())
            {
                var situacaofilter = Builders<ClienteSchema>.Filter.In(x => x.Situacao, situacao);
                filter &= situacaofilter;

            }

            var clientes = await Task.Run(() => DbSet.Find(filter)
            .SortBy(e=> e.DataCriacao)
            .ToList());

            return _mapper.Map<IEnumerable<Cliente>>(clientes);
        }

        #region TesteObterPorId

        //public override async Task<Cliente> ObterPorId(string id)
        //{
        //    var document = await Task.Run(() => (from e in DbSet.AsQueryable()
        //                                         where e.Id == id
        //                                         select new ClienteSchema
        //                                         {
        //                                             Id = e.Id,
        //                                             Cpf = e.Cpf,
        //                                         }).FirstOrDefault());

        //    return _mapper.Map<Cliente>(document);
        //}

        #endregion TesteObterPorId
    }
}