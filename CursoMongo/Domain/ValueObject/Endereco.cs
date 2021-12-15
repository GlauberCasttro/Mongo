using FluentValidation;
using FluentValidation.Results;
using System;

namespace Domain.Entities
{
    public class Endereco
    {
        public Endereco(string logradouro, string numero, string cidade, string uf, string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            Uf = uf;
            Cep = cep;
        }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }
        public string Cep { get; private set; }


        public ValidationResult Validar() => new EnderecoValidation().Validate(this);
    }
}