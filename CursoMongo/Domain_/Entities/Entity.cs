using System;

namespace Domain_.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public DateTime DataCricao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}