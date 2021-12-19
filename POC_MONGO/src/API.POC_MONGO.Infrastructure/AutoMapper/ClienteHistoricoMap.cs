using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Infrastructure.Schemas;
using AutoMapper;

namespace API.POC_MONGO.Infrastructure.AutoMapper
{
    public class ClienteHistoricoMap : Profile
    {
        public ClienteHistoricoMap()
        {
            CreateMap<HistoricoClienteSchema, HistoricoCliente>().ReverseMap();
        }
    }
}