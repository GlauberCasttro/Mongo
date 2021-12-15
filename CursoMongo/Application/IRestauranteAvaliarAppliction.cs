using Application.Dto;
using AutoMapper;
using Domain.Interfaces;
using Domain.ValueObject;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IRestauranteAvaliarAppliction : IValidation
    {
        public Task AvaliarRestaurante(string restauranteId, AvalicacaoDto avalicacao);
    }

    public class RestauranteAvaliarAppliction : IRestauranteAvaliarAppliction
    {
        private readonly IRestauranteRepository _restauranteRepository;
        private readonly IMapper _mapper;
        public bool Invalid { get; private set; }

        public List<ValidationFailure> Errors { get; private set; }

        public RestauranteAvaliarAppliction(IRestauranteRepository restauranteRepository, IMapper mapper)
        {
            _restauranteRepository = restauranteRepository;
            _mapper = mapper;
        }

        public async Task AvaliarRestaurante(string restauranteId, AvalicacaoDto avalicacaoDto)
        {
            var avaliacao = _mapper.Map<Avaliacao>(avalicacaoDto);
            if (!avaliacao.Validar())
            {
                AdicionarErros(avaliacao.Erros);
            }
            await _restauranteRepository.Avaliar2(restauranteId, avaliacao);
        }

        private void AdicionarErros(ValidationResult result)
        {
            Invalid = !result.IsValid;
            Errors = result.Errors;
        }
    }
}