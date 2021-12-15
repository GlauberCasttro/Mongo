using AutoMapper;
using Domain.ValueObject;
using Infra.Data.Mongo;

namespace Infra.Mappings
{
    public class AvaliacaoSchemaMap : Profile
    {
        public AvaliacaoSchemaMap()
        {
            CreateMap<AvaliacaoSchema, Avaliacao>()
                    .ConstructUsing(e => new Avaliacao(e.Estrelas, e.Comentario));

            CreateMap<Avaliacao, AvaliacaoSchema>()
                .ForMember(destino => destino.Comentario, origem => origem.MapFrom(e => e.Comentarios))
                .ForMember(destino => destino.Estrelas, origem => origem.MapFrom(e => e.Estrelas));
        }
    }
}