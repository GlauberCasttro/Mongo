using API.POC_MONGO.Domain.Entities;
using System;
using System.Collections.Generic;

namespace API.POC_MONGO.Application.Models
{
    public class ClienteModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public int? Ddd { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Segmento { get; set; }
        public Situacao Situacao { get; set; }
        public List<string> Telefones { get; set; }
    }
}
