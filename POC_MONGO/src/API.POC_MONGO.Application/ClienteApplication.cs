using API.POC_MONGO.Application.Interfaces;
using API.POC_MONGO.Application.Models;
using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure.Mongo;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace API.POC_MONGO.Application
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly ITransaction _transaction;
        private readonly IClienteHistoricoRepository _clienteHistoricoRepository;

        public ClienteApplication(IMapper mapper, IClienteRepository clienteRepository, ITransaction transaction, IClienteHistoricoRepository clienteHistoricoRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _transaction = transaction;
            _clienteHistoricoRepository = clienteHistoricoRepository;
        }

        public async Task<Result<Cliente>> Atualizar(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            var historico = new HistoricoCliente
            {
                DataAlteracacao = DateTime.Now,
                NomeCliente = cliente.Nome.NomeCompleto
            };

            await _transaction.ApplyTransaction(async () =>
            {
                await _clienteRepository.Atualizar(cliente);
                await _clienteHistoricoRepository.Adicionar(historico);
            });

            return Result<Cliente>.Ok(cliente);
        }

        public async Task Deletar(string id)
        {
            var cliente = await _clienteRepository.ObterPorId(id);
            if (cliente != null)
            {
                await _clienteRepository.Remover(cliente.Id);
            }
        }

        public async Task<Result<Cliente>> Cadastrar(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);

            await _transaction.ApplyTransaction(async () =>
            {
                await _clienteRepository.Adicionar(cliente);
            });

            return Result<Cliente>.Ok(cliente);
        }
    }
}