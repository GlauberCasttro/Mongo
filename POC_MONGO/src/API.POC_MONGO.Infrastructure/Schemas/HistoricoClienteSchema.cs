using System;

namespace API.POC_MONGO.Infrastructure.Schemas
{
    public class HistoricoClienteSchema : ISchemaRoot
    {
        public string Id { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataAlteracacao { get; set; }
    }
}