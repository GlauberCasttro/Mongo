using Application;
using Application.Dto;
using AutoMapper;
using CrossCutting.Util;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class RestauranteController : ApiControllerBase
    {
        private readonly ILogger<RestauranteController> _logger;
        private readonly IMapper _mapper;
        private readonly IRestauranteAdicionarApplication _restauranteCadastrarApplication;
        private readonly IRestauranteAtualizarApplication _restauranteAtualizarApplication;
        private readonly IRestauranteAvaliarAppliction _restauranteAvaliarApplication;
        private readonly IRestauranteRepository _restauranteRepository;

        public RestauranteController(ILogger<RestauranteController> logger,
                                     IRestauranteAdicionarApplication restauranteApplication,
                                     IRestauranteRepository restauranteRepository,
                                     IMapper mapper,
                                     IRestauranteAtualizarApplication restauranteAtualizarApplication,
                                     IRestauranteAvaliarAppliction restauranteAvaliarApplication)
        {
            _logger = logger;
            _restauranteCadastrarApplication = restauranteApplication;
            _restauranteRepository = restauranteRepository;
            _mapper = mapper;
            _restauranteAtualizarApplication = restauranteAtualizarApplication;
            _restauranteAvaliarApplication = restauranteAvaliarApplication;
        }

        /// <summary>
        /// Inserir restaurante
        /// </summary>
        /// <returns>Inseri Restaurante</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPost("adicionar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Adicionar(RestauranteDto restauranteDto)
        {
            await _restauranteCadastrarApplication.Cadastrar(restauranteDto);

            if (_restauranteCadastrarApplication.Invalid) return BadRequest(new
            {
                erros = _restauranteCadastrarApplication?.Errors
                .Select(e => new { property = e.PropertyName, erro = e.ErrorMessage })
            });

            return Ok();
        }

        /// <summary>
        /// Lista restaurante
        /// </summary>
        /// <returns>Lista Restaurante</returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("obter-todos")]
        [ProducesResponseType(typeof(IEnumerable<RestauranteDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos()
        {
            var restaurante = await _restauranteRepository.ObterTodos();

            return Ok(_mapper.Map<IEnumerable<RestauranteDto>>(restaurante));
        }

        [HttpGet]
        [Route("obter-por-nome/{nome}")]
        [ProducesResponseType(typeof(IEnumerable<RestauranteDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos(string nome)
        {
            var restaurante = await _restauranteRepository.ObterPorNome(nome);

            return Ok(_mapper.Map<IEnumerable<RestauranteDto>>(restaurante));
        }

        /// <summary>
        /// Obter por id
        /// </summary>
        /// <returns>Obter restaurante por id</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet("{id}/obter-por-id")]
        [ProducesResponseType(typeof(RestauranteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(string id)
        {
            if (!id.ValidarBSON()) return BadRequest(new { Error = "Formato do identificador inválido." });

            var restaurante = await _restauranteRepository.ObterPorId(id);

            if (restaurante == null) return NotFound();

            return Ok(_mapper.Map<RestauranteDto>(restaurante));
        }

        [HttpPut("atualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(RestauranteDto restauranteDto)
        {
            await _restauranteAtualizarApplication.Atualizar(restauranteDto);

            if (_restauranteAtualizarApplication.Invalid) return BadRequest(new
            {
                erros = _restauranteAtualizarApplication?.Errors
                .Select(e => new { property = e.PropertyName, erro = e.ErrorMessage })
            });

            return Ok();
        }

        [HttpPatch("{id}/atualizar-cozinha")]
        public async Task<IActionResult> AtualizarCozinha(string id, int cozinha)
        {
            if (!EnumValidation.IsEnum((Cozinha)cozinha)) return BadRequest(new { Error = "Enum inválido" });
            if (!id.ValidarBSON()) return BadRequest(new { Error = "Formato do identificador inválido." });

            var resuult = await _restauranteRepository.AtualizarCozinha(id, (Cozinha)cozinha);
            if (!resuult) return NotAccepted(new { Info = "Nenhum registro atualizado." });

            return Ok();
        }

        [HttpPatch("/avaliar")]
        public async Task<IActionResult> AdicionarAvaliacao(string restauranteId, AvalicacaoDto avaliacaoDto)
        {
            await _restauranteAvaliarApplication.AvaliarRestaurante(restauranteId, avaliacaoDto);
            if (_restauranteAvaliarApplication.Invalid) return BadRequest(new
            {
                Erros = _restauranteAvaliarApplication.Errors?.Select(e => new
                {
                    Proprerty = e.PropertyName,
                    Erro = e.ErrorMessage
                })
            }) ;

            return Ok();
        }
    }
}