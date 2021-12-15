using Application.Dto;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public interface IRestauranteAtualizarApplication : IValidation
    {
        Task Atualizar(RestauranteDto restaurante);
        Task<bool> AlterarCozinha(string id, Cozinha cozinha);
    }
}