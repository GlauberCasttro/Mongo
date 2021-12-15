using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class RestauranteAtualizarApplication : IRestauranteAtualizarApplication
    {
        public bool Invalid { get; private set; }

        private readonly IMapper _mapper;
        private readonly IRestauranteRepository _restauranteRepository;
        public List<ValidationFailure> Errors { get; private set; }

        public RestauranteAtualizarApplication(IMapper mapper, IRestauranteRepository restauranteRepository)
        {
            _mapper = mapper;
            _restauranteRepository = restauranteRepository;
        }

        public async Task Atualizar(RestauranteDto restauranteDto)
        {
            var restauranteBase = _mapper.Map<Restaurante>(await _restauranteRepository.ObterPorId(restauranteDto.Id));

            var restaurante = new Restaurante(restauranteBase.Id, restauranteDto.Cozinha,
                new Endereco(restauranteDto.Logradouro,
                restauranteDto.Numero,
                restauranteDto.Cidade,
                restauranteDto.Uf,
                restauranteDto.Cep));

            var validate = restaurante.Validar();

            if (!validate.IsValid)
            {
                AdicionarErros(validate);
                return;
            }

            var statusAtualizacao = await _restauranteRepository.AtualizarCompleto(restaurante);
            await GravarHistorico(restaurante, statusAtualizacao);
        }

        private async Task GravarHistorico(Restaurante restaurante, bool atualizacaoRealizada)
        {
            if (atualizacaoRealizada)
            {
                restaurante.ArmazenarHistorico(new HistoricoRestaurante
                {
                    Cozinha = restaurante.Cozinha,
                    DataHistorico = DateTime.Now,
                    Nome = restaurante.Nome
                });
                await _restauranteRepository.AtualizarHistorico(restaurante.Id, restaurante.Historico);
            }
        }

        public async Task<bool> AlterarCozinha(string id, Cozinha cozinha)
        {
            return await _restauranteRepository.AtualizarCozinha(id, cozinha);
        }

        private void AdicionarErros(ValidationResult result)
        {
            Invalid = !result.IsValid;
            Errors = result.Errors;
        }
    }
}