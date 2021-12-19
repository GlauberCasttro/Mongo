using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.POC_MONGO.Domain.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Adicionar(TEntity obj);

        Task<TEntity> ObterPorId(string id);

        Task<IEnumerable<TEntity>> ObterTodos();

        Task Atualizar(TEntity obj);

        Task Remover(string id);
    }
    public interface IRepository_2<TEntity> : IDisposable where TEntity : class
    { }
}