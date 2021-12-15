using Domain.ValueObject;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IAvalicacaoRepository
    {
        Task IncluirAvalicacao(string restauranteId, Avaliacao avalicacao);
    }
}