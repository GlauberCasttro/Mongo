using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Domain.Entities;
using System.Threading.Tasks;

namespace API.POC_MONGO.Domain.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    { }

    public interface IClienteRepository_2 : IRepository_2<Cliente>
    {
        Task Adicionar(Cliente cliente);
        Task AtualizarCompleto(Cliente restaurante);
    }
}