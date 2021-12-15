using Domain_.Interface;
using System;

namespace Domain_.Entities
{
    public class Veiculos : Entity, IAggregateRoot
    {
        public Veiculos(string placa, string chassi, DateTime dataCompra, int anoFabricacao, int fabricante, string marca, string modelo)
        {
            Placa = placa;
            Chassi = chassi;
            DataCompra = dataCompra;
            AnoFabricacao = anoFabricacao;
            Fabricante = fabricante;
            Marca = marca;
            Modelo = modelo;
        }

        public string Placa { get; private set; }
        public string Chassi { get; private set; }
        public DateTime DataCompra { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int Fabricante { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
    }
}