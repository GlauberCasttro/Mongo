using API.POC_MONGO.Application.Interfaces;
using API.POC_MONGO.Application.Models;
using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.POC_MONGO.Api.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IClienteApplication _clienteApplication;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IMapper mapper, IClienteApplication clienteApplication, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteApplication = clienteApplication;
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var cliente = await _clienteRepository.ObterPorId(id);
            _z

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            return Ok(_mapper.Map<Cliente, ClienteModel>(cliente));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var cliente = await _clienteRepository.ObterTodos();

            return Ok(_mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteModel>>(cliente));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ClienteModel clienteModel)
        {
            var result = await _clienteApplication.Cadastrar(clienteModel);

            if (result.Success)
                return Created($"/clientes/{result.Object.Id}", _mapper.Map<Cliente, ClienteModel>(result.Object));

            return BadRequest(result.Notifications);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(ClienteModel clienteModel)
        {
            var result = await _clienteApplication.Atualizar(clienteModel);

            if (result.Success)
                return Ok();

            return BadRequest(result.Notifications);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            await _clienteApplication.Deletar(id);

            return NoContent();
        }
    }
}