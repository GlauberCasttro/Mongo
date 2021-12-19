using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.POC_MONGO.Tests.Mocks
{
    public class ClienteRepositoryMock : IClienteRepository
    {
        public void Incluir(Cliente cliente)
        {
        }

        public Cliente ObterPorId(Guid id)
        {
            return Clientes.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            return Clientes;
        }

        private static IEnumerable<Cliente> Clientes
        {
            get
            {
                var cliente1 = new Cliente(
                        new Nome("João", "Silva"),
                        new CPF("12345678910"),
                        new Email("joaosilva@gmail.com"),
                        "abc123");

                var cliente2 = new Cliente(
                        new Nome("Maria", "Silva"),
                        new CPF("01987654321"),
                        new Email("mariasilva@gmail.com"),
                        "xpz789");

                typeof(Cliente).GetProperty("Id").SetValue(cliente1, Guid.Parse("f8a0db6b-dabf-4f97-9b1c-8cf08b930466"));
                typeof(Cliente).GetProperty("DataCriacao").SetValue(cliente1, new DateTime(2011, 2, 2));

                typeof(Cliente).GetProperty("Id").SetValue(cliente2, Guid.Parse("6fdc66ad-649f-4f3c-9806-6409e8ca4e47"));
                typeof(Cliente).GetProperty("DataCriacao").SetValue(cliente2, new DateTime(2010, 1, 1));

                return new Cliente[]
                {
                        cliente1, cliente2
                };
            }
        }
    }
}
