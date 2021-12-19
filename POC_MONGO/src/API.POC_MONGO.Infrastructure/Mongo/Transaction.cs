using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public class Transaction : ITransaction
    {
        private readonly IClientSessionHandle _clientSessionHandle;

        public Transaction(IClientSessionHandle clientSessionHandle) =>
            (_clientSessionHandle) = (clientSessionHandle);

        public async Task ApplyTransaction(Func<Task> method)
        {
            try
            {
                _clientSessionHandle.StartTransaction();
                await method();
                await _clientSessionHandle.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _clientSessionHandle.AbortTransactionAsync();
                throw new Exception("Ocorreu um erro ao executar metodo transacionado.", ex);
            }
        }

        public void Dispose() =>
            _clientSessionHandle?.Dispose();
    }
}