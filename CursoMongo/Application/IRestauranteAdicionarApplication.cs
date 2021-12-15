using Application.Dto;
using System.Threading.Tasks;

namespace Application
{
    public interface IRestauranteAdicionarApplication : IValidation
    {
        Task Cadastrar(RestauranteDto restaurante);
    }
}