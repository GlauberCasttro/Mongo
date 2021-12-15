using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class RestauranteCadastrarApplication : IRestauranteAdicionarApplication
    {
        public bool Invalid{get; private set;}

        private readonly IMapper _mapper;
        private readonly IRestauranteRepository _restauranteRepository;
        public List<ValidationFailure> Errors { get; private set; }

        public RestauranteCadastrarApplication(IMapper mapper, IRestauranteRepository restauranteRepository)
        {
            _mapper = mapper;
            _restauranteRepository = restauranteRepository;
        }

        public async Task Cadastrar(RestauranteDto restauranteDto)
        {
            var restaurante = _mapper.Map<Restaurante>(restauranteDto);
            var validate = restaurante.Validar();

            if (!validate.IsValid)
            {
                AdicionarErros(validate);
                return;
            }

            await _restauranteRepository.Adicionar(restaurante);
        }

        private void AdicionarErros(ValidationResult result)
        {
            Invalid = !result.IsValid;
            Errors = result.Errors;
        }
    }

}