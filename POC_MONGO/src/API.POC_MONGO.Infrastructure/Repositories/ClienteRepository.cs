using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure.Mongo;
using API.POC_MONGO.Infrastructure.Schemas;
using AutoMapper;
using MongoDB.Driver;

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