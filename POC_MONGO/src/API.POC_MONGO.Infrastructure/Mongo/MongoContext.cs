using API.POC_MONGO.Infrastructure.Mongo.Mappings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.POC_MONGO.Infrastructure.Mongo
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;

        public MongoContext(IClientSessionHandle clientSessionHandle)
        {
            Session = clientSessionHandle;
            _commands = new List<Func<Task>>();
        }

        private void GetDatabase()
        {
            if (MongoClient != null)
                return;

            MongoClient = new MongoClient("mongodb+srv://curso-mongo:curso-mongo@clustercursomongo.cw7w0.mongodb.net?retryWrites=true&w=majority");

            Database = MongoClient.GetDatabase("Curso-Mongo");

            MappingSchema.MapearEntidadesMongo();
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            GetDatabase();

            return Database.GetCollection<T>(name);
        }

        public async Task<int> SaveChanges()
        {
            GetDatabase();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public async Task<IClientSessionHandle> InitTransaction()
        {
            Session = await MongoClient.StartSessionAsync();
            Session.StartTransaction();
            return Session;
        }

        public async Task Commit() =>
            await Session.CommitTransactionAsync();

        public void AddCommand(Func<Task> func) => _commands.Add(func);

        public void Dispose() => Session?.Dispose();
    }
}