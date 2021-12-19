using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.ValueObjects;
using API.POC_MONGO.Infrastructure.Schemas;
using AutoMapper;

namespace API.POC_MONGO.Infrastructure.AutoMapper
{
    public class ClienteSchemaMap : Profile
    {
        public ClienteSchemaMap()
        {
            CreateMap<ClienteSchema, Cliente>()
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ForMember(dest => dest.Cpf, m => m.Ignore())
                .ForMember(dest => dest.Email, m => m.Ignore())
                .ForMember(dest => dest.Telefone, m => m.MapFrom(src => src.Ddd.HasValue ? new Telefone(src.Ddd.Value, src.Telefone) : null))
                .ForMember(dest=> dest.Telefones, m => m.MapFrom(src=> src.Telefones))
                .ConstructUsing(src =>
                    new Cliente(
                        new Nome(src.Nome, src.Sobrenome),
                        new CPF(src.Cpf),
                        new Email(src.Email),
                        src.Segmento)
                    );


            CreateMap<Cliente, ClienteSchema>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome.PrimeiroNome))
                .ForMember(dest => dest.Sobrenome, m => m.MapFrom(src => src.Nome.Sobrenome))
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.ToString()))
                .ForMember(dest => dest.Ddd, m => m.MapFrom(src => src.Telefone.Ddd))
                .ForMember(dest => dest.Telefone, m => m.MapFrom(src => src.Telefone.Numero))
                .ForMember(dest => dest.Telefones, m => m.MapFrom(src => src.Telefones))
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.Email.ToString()));
        }
    }
}