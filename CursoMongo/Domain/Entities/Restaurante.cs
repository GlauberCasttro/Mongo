using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Restaurante : Entity
    {
        public Restaurante(string id, string nome, Cozinha cozinha, Endereco endereco)
        {
            Id = id;
            Nome = nome;
            Cozinha = cozinha;
            Endereco = endereco;
            Historico = new List<HistoricoRestaurante>();
        }

        public Restaurante(string nome, Cozinha cozinha, Endereco endereco)
        {
            Nome = nome;
            Cozinha = cozinha;
            Endereco = endereco;
            Historico = new List<HistoricoRestaurante>();
        }

        public string Nome { get; private set; }
        public Cozinha Cozinha { get; private set; }
        public Endereco Endereco { get; private set; }
        public IList<HistoricoRestaurante> Historico { get; private set; }

        public ValidationResult Validar()
        {
            var restauranteValidate = new RestauranteValidate().Validate(this);
            var enderecoValido = Endereco.Validar();

            if (!enderecoValido.IsValid)
                restauranteValidate.Errors.AddRange(enderecoValido.Errors);

            return restauranteValidate;
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            if (Endereco == null) throw new Exception("É necessário informar o endereço para atualização.");

            Validar();
            Endereco = endereco;
        }

        public void ArmazenarHistorico(HistoricoRestaurante historicoRestaurante)
        {
            Historico.Add(historicoRestaurante);
        }
    }
}