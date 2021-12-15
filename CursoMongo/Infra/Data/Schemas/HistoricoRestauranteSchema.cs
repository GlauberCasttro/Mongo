using Domain.Entities;
using Infra.Data.Schemas;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infra.Data.Mongo
{
    public class HistoricoRestauranteSchema : ISchemaMongoBase
    {
        public string Nome { get; set; }
        public Cozinha Cozinha { get; set; }
        public DateTime DataHistorico { get; set; }
    }

    public class AvaliacaoSchema : ISchemaMongoBase
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RestauranteId { get; set; }

        public int Estrelas { get; set; }
        public string Comentario { get; set; }
    }
}