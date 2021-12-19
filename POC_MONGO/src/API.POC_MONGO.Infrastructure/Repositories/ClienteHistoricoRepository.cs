using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure.Mongo;
using API.POC_MONGO.Infrastructure.Schemas;
using AutoMapper;
using MongoDB.Driver;

namespace API.POC_MONGO.Infrastructure.Repositories
{
    public class ClienteHistoricoRepository : BaseRepository<HistoricoCliente, HistoricoClienteSchema>, IClienteHistoricoRepository
    {
        public ClienteHistoricoRepository(IMapper mapper, IMongoContext context, IClientSessionHandle clientSessionHandle)
            : base(context, mapper, clientSessionHandle, "historico_clientes")
        { }
    }
}