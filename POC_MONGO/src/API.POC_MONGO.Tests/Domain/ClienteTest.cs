using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.ValueObjects;
using System;
using Xunit;

namespace API.POC_MONGO.Tests.Domain
{
    public class ClienteTest
    {
        [Fact]
        public void CriarCliente_ClienteInvalido_Test()
        {
            var cliente = new Cliente(null, null, null);

            Assert.True(cliente.Invalid);
            Assert.Contains(cliente.Notifications, n => n.Property == nameof(Cliente.Nome));
            Assert.Contains(cliente.Notifications, n => n.Property == nameof(Cliente.Cpf));
            Assert.Contains(cliente.Notifications, n => n.Property == nameof(Cliente.Email));
        }

        [Fact]
        public void CriarCliente_ClienteValido_Test()
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            Assert.True(cliente.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc4567")]
        [InlineData("123456")]
        [InlineData("abcdef")]
        [InlineData("abcd12")]
        [InlineData("ab1234")]
        public void CriarCliente_SegmentoInvalido_Test(string segmento)
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"),
                segmento);

            Assert.True(cliente.Invalid);
            Assert.Contains(cliente.Notifications, n => n.Property == nameof(Cliente.Segmento));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc4567")]
        [InlineData("123456")]
        [InlineData("abcdef")]
        [InlineData("abcd12")]
        [InlineData("ab1234")]
        public void AlterarSegmento_Invalido_Test(string segmento)
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            cliente.AlterarSegmento(segmento);

            Assert.True(cliente.Invalid);
            Assert.Contains(cliente.Notifications, n => n.Property == nameof(Cliente.Segmento));
        }

        [Theory]
        [InlineData("abc123")]
        [InlineData("XYZ789")]
        [InlineData(null)]
        public void AlterarSegmento_Valido_Test(string segmento)
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            cliente.AlterarSegmento(segmento);

            Assert.True(cliente.Valid);
        }

        [Theory]
        [InlineData(31, "988882233")]
        [InlineData(11, "999992211")]
        public void InformarOuAlterarTelefone_Valido_Test(int ddd, string numero)
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            cliente.InformarOuAlterarTelefone(new Telefone(ddd, numero));

            Assert.True(cliente.Valid);
        }

        [Fact]
        public void InformarOuAlterarTelefone_NuloValido_Test()
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            cliente.InformarOuAlterarTelefone(null);

            Assert.True(cliente.Valid);
        }

        [Theory]
        [InlineData(0, "9888821AA")]
        [InlineData(31, null)]
        public void InformarOuAlterarTelefone_Invalido_Test(int ddd, string numero)
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            cliente.InformarOuAlterarTelefone(new Telefone(ddd, numero));

            Assert.True(cliente.Invalid);
            Assert.Contains(cliente.Notifications, n => n.Property == nameof(Cliente.Telefone.Numero));
        }

        [Fact]
        public void CriarCliente_DataCriacao_Test()
        {
            var cliente = new Cliente(
                new Nome("João", "Silva"),
                new CPF("12345678911"),
                new Email("joao@localiza.com"));

            Assert.Equal(DateTime.Today, cliente.DataCriacao.Date);
        }
    }
}
