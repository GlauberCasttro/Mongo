using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObject;

namespace Application.AutoMapper
{
    //Pode usar o implicit operator https://github.com/fabiogalante/ImplicitOperator
    public class RestauranteMapper : Profile
    {
        public RestauranteMapper()
        {
            CreateMap<RestauranteDto, Restaurante>()
                .ConstructUsing(rest => new Restaurante(rest.Nome, rest.Cozinha, new Endereco(rest.Logradouro, rest.Numero, rest.Cidade, rest.Uf, rest.Cep)));

            CreateMap<Restaurante, RestauranteDto>()
                .ForMember(destino => destino.Id, origem => origem.MapFrom(e => e.Id))
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(e => e.Nome))
                .ForMember(destino => destino.Cozinha, origem => origem.MapFrom(e => e.Cozinha))
                .ForMember(destino => destino.Logradouro, origem => origem.MapFrom(e => e.Endereco.Logradouro))
                .ForMember(destino => destino.Numero, origem => origem.MapFrom(e => e.Endereco.Numero))
                .ForMember(destino => destino.Cidade, origem => origem.MapFrom(e => e.Endereco.Cidade))
                .ForMember(destino => destino.Cep, origem => origem.MapFrom(e => e.Endereco.Cep))
                .ForMember(destino => destino.Uf, origem => origem.MapFrom(e => e.Endereco.Uf));
        }
    }

    public class AvaliacaoMaP : Profile
    {
        public AvaliacaoMaP()
        {
            CreateMap<AvalicacaoDto, Avaliacao>()
                .ConstructUsing(e => new Avaliacao(e.Estrela, e.Comentario));
        }
    }
}