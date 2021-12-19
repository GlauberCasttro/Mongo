using Microsoft.AspNetCore.Mvc;
using API.POC_MONGO.Api.Controllers;
using API.POC_MONGO.Application;
using API.POC_MONGO.Application.Models;
using API.POC_MONGO.Tests.Fixtures;
using API.POC_MONGO.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace API.POC_MONGO.Tests.Controllers
{
    [Collection("Mapper")]
    public class ClienteControllerTest
    {
        private readonly MapperFixture _mapperFixture;

        public ClienteControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }

        [Theory]
        [InlineData("f8a0db6b-dabf-4f97-9b1c-8cf08b930466")]
        [InlineData("6fdc66ad-649f-4f3c-9806-6409e8ca4e47")]
        public void ObterDadosCliente_ClientesExistentes_Test(string id)
        {
            var controller = CreateClienteController();
            var result = controller.Get(Guid.Parse(id));

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("42f41603-5269-4c0d-9ce2-afa8d293240b")]
        [InlineData("3dd64d9c-aaef-4a98-9b2c-5f6d7fc28ead")]
        public void ObterDadosCliente_ClientesInexistentes_Test(string id)
        {
            var controller = CreateClienteController();
            var result = controller.Get(Guid.Parse(id));

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void ListarClientes_ClientesCadastrados_Test()
        {
            var controller = CreateClienteController();
            var result = controller.List();

            Assert.IsType<OkObjectResult>(result);
            
            var objectResult = GetOkObject<IEnumerable<ClienteModel>>(result);
            Assert.Equal(2, objectResult.Count());
        }

        [Fact]
        public void InserirCliente_ClienteInvalido_Test()
        {
            var model = new ClienteModel()
            {
                Nome = "João",
                Sobrenome = "Silva"
            };

            var controller = CreateClienteController();
            var result = controller.Post(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void InserirCliente_ClienteValido_Test()
        {
            var model = new ClienteModel()
            {
                Nome = "João",
                Sobrenome = "Silva",
                Cpf = "12345678910",
                Ddd = 11,
                Telefone = "999009900",
                Email = "joao@gmail.com",
                Segmento = "aqw187"
            };

            var controller = CreateClienteController();
            var result = controller.Post(model);

            Assert.IsType<CreatedResult>(result);
        }

        private ClienteController CreateClienteController()
        {
            var clienteRepository = new ClienteRepositoryMock();
            var clienteApplication = new ClienteApplication(_mapperFixture.Mapper, clienteRepository);

            return new ClienteController(_mapperFixture.Mapper, clienteApplication, clienteRepository);
        }

        private T GetOkObject<T>(IActionResult result)
        {
            var okObjectResult = (OkObjectResult)result;
            return (T)okObjectResult.Value;
        }
    }
}
