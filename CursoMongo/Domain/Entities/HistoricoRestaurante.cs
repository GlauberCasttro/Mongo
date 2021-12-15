using System;

namespace Domain.Entities
{
    public class HistoricoRestaurante
    {
        public string Nome { get; set; }
        public Cozinha Cozinha { get; set; }
        public DateTime DataHistorico { get; set; }
    }
}