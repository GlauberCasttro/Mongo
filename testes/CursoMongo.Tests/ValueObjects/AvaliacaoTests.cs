using Domain.ValueObject;
using Xunit;

namespace CursoMongo.Tests.ValueObjects
{
    public class AvaliacaoTests
    {
        [Fact]
        public void Avaliacao_Validar_NaoDeveRetornarErros()
        {
            //Arrange
            var avaliacao = new Avaliacao(1, "Teste");

            //Act
            avaliacao.Validar();

            //Assert
            Assert.True(avaliacao.Erros.IsValid);
            Assert.True(avaliacao.Erros.Errors.Count == 0);
        }

        [Fact]
        public void Avaliacao_Validar_DeveRetornarErrosQuandoInvalida()
        {
            //Arrange
            var avaliacao = new Avaliacao(0, "");

            //Act
            avaliacao.Validar();

            //Assert
            Assert.False(avaliacao.Erros.IsValid);
            Assert.Collection(avaliacao.Erros.Errors,
            item => Assert.Equal("Número de estrelas deve ser maior que zero", item.ErrorMessage),
            item => Assert.Equal("Comentario não pode ser vazio", item.ErrorMessage));
        }
    }
}