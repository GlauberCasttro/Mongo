namespace Infra.Data.Schemas
{
    public class EnderecoSchema : ISchemaMongoBase
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
    }
}
