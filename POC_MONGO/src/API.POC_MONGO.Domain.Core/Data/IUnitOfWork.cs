using System;
using System.Threading.Tasks;

namespace API.POC_MONGO.Domain.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}