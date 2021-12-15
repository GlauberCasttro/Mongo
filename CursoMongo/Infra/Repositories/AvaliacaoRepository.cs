using AutoMapper;
using Domain.ValueObject;
using Infra.Data.Mongo;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class AvaliacaoRepository : RepositoryMongoBase<AvaliacaoSchema>, IAvalicacaoRepository
    {
        public const string COLLECTION_NAME = "avaliacoes";
        private readonly IMapper _mapper;

        public AvaliacaoRepository(MongoDbContext mongo, IMapper mapper) : base(mongo, COLLECTION_NAME)
        {
            _mapper = mapper;
        }

        public async Task IncluirAvalicacao(string restauranteId, Avaliacao avalicacao)
        {
            var avaliacaoSchema = _mapper.Map<AvaliacaoSchema>(avalicacao);

            await _collection.InsertOneAsync(avaliacaoSchema);
        }
    }
}