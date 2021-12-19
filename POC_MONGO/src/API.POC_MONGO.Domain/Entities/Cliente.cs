using API.POC_MONGO.Domain.Core.Entities;
using API.POC_MONGO.Domain.ValueObjects;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace API.POC_MONGO.Domain.Entities
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(Nome nome, CPF cpf, Email email)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            DataCriacao = DateTime.Now;
            Telefones = new List<string>();

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Cpf, nameof(Cpf), "Cpf não pode ser nulo")
                .IsNotNull(Email, nameof(Email), "Email não pode ser nulo"));

            if (Nome != null)
                AddNotifications(Nome);

            if (Cpf != null)
                AddNotifications(Cpf);

            if (Email != null)
                AddNotifications(Email);
        }

        public Cliente(Nome nome, CPF cpf, Email email, string segmento)
            : this(nome, cpf, email)
        {
            AlterarSegmento(segmento);
        }

        public Nome Nome { get; private set; }
        public CPF Cpf { get; private set; }
        public Telefone Telefone { get; private set; }
        public Email Email { get; private set; }
        public string Segmento { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Situacao Situacao { get; private set; }
        public List<string> Telefones { get; private set; }

        public void AlterarSituacao(Situacao situacao)
        {
            Situacao = situacao;
        }

        public void AdicionarTelefone(string telefone)
        {
            if (!Telefones.Contains(telefone))
                Telefones.Add(telefone);
        }

        public void InformarOuAlterarTelefone(Telefone telefone)
        {
            Telefone = telefone;

            if (Telefone != null)
                AddNotifications(telefone);
        }

        public void AlterarSegmento(string segmento)
        {
            Segmento = segmento;

            if (Segmento == null)
                return;

            AddNotifications(new Contract()
                .Requires()
                .HasLen(Segmento, 6, nameof(Segmento), "Segmento deve conter 6 posições")
                .Matchs(Segmento, "^[a-zA-Z]{3}[0-9]{3}$", nameof(Segmento), "Segmento está fora do padrão de nomenclatura"));
        }
    }
}