using Domain.Entities;
using Infra.Data.Schemas;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Infra.Data.Mongo
{
    public class RestauranteSchema : ISchemaMongoBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public Cozinha Cozinha { get; set; }
        public EnderecoSchema EnderecoSchema { get; set; }
        public IList<HistoricoRestauranteSchema> Historico { get; set; }
    }
}