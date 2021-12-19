using System;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public interface ITransaction : IDisposable
    {
        Task ApplyTransaction(Func<Task> method);
    }
}