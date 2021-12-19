using API.POC_MONGO.Domain.Core.Data;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        public UnitOfWork(IMongoContext context) => _context = context;

        public async Task<bool> Commit() => await _context.SaveChanges() > 0;

        public void Dispose() => _context.Dispose();
    }
}