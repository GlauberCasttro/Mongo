using Domain.Entities;
using Domain.ValueObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRestauranteRepository
    {
        Task Adicionar(Restaurante restaurante);

        Task<IEnumerable<Restaurante>> ObterTodos();

        Task<Restaurante> ObterPorId(string id);

        Task<bool> AtualizarCompleto(Restaurante restaurante);

        Task<bool> AtualizarCozinha(string id, Cozinha cozinha);

        Task<bool> AtualizarHistorico(string id, IList<HistoricoRestaurante> historico);

        Task<bool> BuscarHistoricoPorCozinha(string id, Cozinha cozinha);
        Task<IEnumerable<Restaurante>> ObterPorNome(string nome);
        Task Avaliar2(string restauranteId, Avaliacao avaliacao);
    }
}