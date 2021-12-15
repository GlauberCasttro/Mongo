using Domain.Entities;
using Xunit;

namespace CursoMongo.Tests.Domain
{
    public class RestauranteTests
    {
        [Fact(DisplayName = "Restaurante Inválido")]
        public void Restaurante_Validate_DeveRetornarMensagenDeValidacaoInavlidandoAEntidade()
        {
            //Arrange
            var endereco = new Endereco("Rua rio grande do sul", "646", "belo Horizonte", "MG", "30170110");
            var resturante = new Restaurante(id: null, nome: null, (Cozinha)3, endereco);

            //Act
            var validation = resturante.Validar();

            //Assert
            Assert.False(validation.IsValid);

            Assert.Collection(validation.Errors,
                erro => Assert.Contains("Nome é obrigatorio.", erro.ErrorMessage),
                erro => Assert.Contains("Cozinha não valida.", erro.ErrorMessage)
               );

            Assert.True(validation.Errors.Count > 0);
            Assert.True(validation.Errors.Count == 2);
        }

        [Fact(DisplayName = "Restaurante e Enderço Inválido")]
        public void Restaurante_Endereco_Validate_DeveRetornarMensagenDeValidacaoInavlidandoAEntidade_Endereco()
        {
            //Arrange
            var endereco = new Endereco(null, "646", "belo Horizonte", "MG", null);
            var resturante = new Restaurante(id: null, nome: null, (Cozinha)3, endereco);

            //Act
            var validation = resturante.Validar();

            //Assert
            Assert.False(validation.IsValid);

            Assert.Collection(validation.Errors,
                erro => Assert.Contains("Nome é obrigatorio.", erro.ErrorMessage),
                erro => Assert.Contains("Cozinha não valida.", erro.ErrorMessage),
                erro => Assert.Contains("Logradouro não poder ser vazio.", erro.ErrorMessage),
                erro => Assert.Contains("O campo cep não pode ser vazio.", erro.ErrorMessage)
               );

            Assert.True(validation.Errors.Count > 0);
            Assert.True(validation.Errors.Count == 4);
        }
    }
}