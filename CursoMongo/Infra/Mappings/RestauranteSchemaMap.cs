using AutoMapper;
using Domain.Entities;
using Infra.Data.Mongo;

namespace Infra.Mappings
{
    /// <summary>
    /// mapeamento do schema mongo para entidade de dominio
    /// </summary>
    public class RestauranteSchemaMap : Profile
    {
        public RestauranteSchemaMap()
        {
            //Mapeando de restaurante schema para restaurante
            CreateMap<RestauranteSchema, Restaurante>()
                .ConstructUsing(rest => new Restaurante(rest.Nome, rest.Cozinha,
                new Endereco(rest.EnderecoSchema.Logradouro,
                rest.EnderecoSchema.Numero, rest.EnderecoSchema.Cidade, rest.EnderecoSchema.Uf, rest.EnderecoSchema.Cep)))
                .ForMember(e => e.Historico, origem => origem.MapFrom(e => e.Historico));


            //Mapeando de restaurante para restauranteSchema
            CreateMap<Restaurante, RestauranteSchema>()
                .ForMember(destino => destino.Id, origem => origem.MapFrom(e => e.Id))
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(e => e.Nome))
                .ForMember(destino => destino.Historico, origem => origem.MapFrom(e => e.Historico))
                .ForMember(destino => destino.Cozinha, origem => origem.MapFrom(e => e.Cozinha))
                .ForPath(destino => destino.EnderecoSchema.Logradouro, origem => origem.MapFrom(e => e.Endereco.Logradouro))
                .ForPath(destino => destino.EnderecoSchema.Numero, origem => origem.MapFrom(e => e.Endereco.Numero))
                .ForPath(destino => destino.EnderecoSchema.Cidade, origem => origem.MapFrom(e => e.Endereco.Cidade))
                .ForPath(destino => destino.EnderecoSchema.Cep, origem => origem.MapFrom(e => e.Endereco.Cep))
                .ForPath(destino => destino.EnderecoSchema.Uf, origem => origem.MapFrom(e => e.Endereco.Uf));

            CreateMap<HistoricoRestaurante, HistoricoRestauranteSchema>().ReverseMap();
        }
    }
}