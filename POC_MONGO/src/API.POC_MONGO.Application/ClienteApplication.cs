using API.POC_MONGO.Application.Interfaces;
using API.POC_MONGO.Application.Models;
using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure.Mongo;
using AutoMapper;
using System.Threading.Tasks;

namespace API.POC_MONGO.Application
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMongoContext

        public ClienteApplication(IMapper mapper, IClienteRepository clienteRepository, IUnitOfWork uow)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _uow = uow;
        }

        public async Task<Result<Cliente>> Atualizar(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);

            await _clienteRepository.Atualizar(cliente);
            await _uow.Commit();

            return Result<Cliente>.Ok(cliente);
        }

        public async Task Deletar(string id)
        {
            var cliente = await _clienteRepository.ObterPorId(id);
            if (cliente != null)
            {
                await _clienteRepository.Remover(cliente.Id);
                await _uow.Commit();
            }
        }

        public async Task<Result<Cliente>> Salvar(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            await _clienteRepository.Adicionar(cliente);
            await _uow.Commit();

            return Result<Cliente>.Ok(cliente);
        }
    }
}