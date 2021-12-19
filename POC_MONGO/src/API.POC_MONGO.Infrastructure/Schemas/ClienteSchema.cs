using API.POC_MONGO.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace API.POC_MONGO.Infrastructure.Schemas
{
    public class ClienteSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public int? Ddd { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Segmento { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<string> Telefones { get; set; }
    }
}