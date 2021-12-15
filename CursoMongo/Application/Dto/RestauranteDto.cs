using Domain.Entities;

namespace Application.Dto
{
    public class RestauranteDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public Cozinha Cozinha { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
    }

    public class AvalicacaoDto
    {
        public string Comentario { get; set; }
        public int Estrela { get; set; }
    }
}