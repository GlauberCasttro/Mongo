using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.Results;
using MongoDB.Driver;
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
        private readonly IClientSessionHandle clientSessionHandle;

        public RestauranteCadastrarApplication(IMapper mapper, IRestauranteRepository restauranteRepository, 
            IClientSessionHandle clientSessionHandle)
        {
            this.clientSessionHandle = clientSessionHandle;
               _mapper = mapper;
            _restauranteRepository = restauranteRepository;
        }

        public async Task Cadastrar(RestauranteDto restauranteDto)
        {
            var restaurante = _mapper.Map<Restaurante>(restauranteDto);
            //var validate = restaurante.Validar();

            //if (!validate.IsValid)
            //{
            //    AdicionarErros(validate);
            //    return;
            //}

            clientSessionHandle.StartTransaction();

            await _restauranteRepository.Adicionar(restaurante);

            await clientSessionHandle.CommitTransactionAsync();
        }

        private void AdicionarErros(ValidationResult result)
        {
            Invalid = !result.IsValid;
            Errors = result.Errors;
        }
    }

}