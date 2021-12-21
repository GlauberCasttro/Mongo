using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.POC_MONGO.Domain.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> Obter(string nome, Situacao situacao);
        Task<IEnumerable<Cliente>> Obter(string nome, IEnumerable<Situacao> situacao);
    }
}