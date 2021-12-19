using API.POC_MONGO.Application.Models;
using API.POC_MONGO.Domain.Entities;
using System.Threading.Tasks;

namespace API.POC_MONGO.Application.Interfaces
{
    public interface IClienteApplication
    {
        Task<Result<Cliente>> Cadastrar(ClienteModel clienteModel);
        Task<Result<Cliente>> Atualizar(ClienteModel clienteModel);

        Task Deletar(string id);
    }
}