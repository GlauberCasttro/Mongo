using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObject;
using Infra.Data.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    /// <summary>
    /// Repositorio da entidade restaurante
    /// </summary>
    public class RestauranteRepository : RepositoryMongoBase<RestauranteSchema>, IRestauranteRepository
    {
        private readonly IMapper _mapper;
        private const string Restaurante = "restaurantes";
        private const string Avaliacao = "avaliacoes";
        private readonly IAvalicacaoRepository _avalicacaoRepository;

        private readonly ICollectionMongoFactory<RestauranteSchema> _restaurantes;
        private readonly ICollectionMongoFactory<AvaliacaoSchema> _avaliacao_restaurante;

        public RestauranteRepository(MongoDbContext mongo,
            IMapper mapper, IAvalicacaoRepository avalicacaoRepository,
            ICollectionMongoFactory<RestauranteSchema> restaurante,
            ICollectionMongoFactory<AvaliacaoSchema> avaliacao_restaurante) : base(mongo, Restaurante)
        {
            _mapper = mapper;
            _avalicacaoRepository = avalicacaoRepository;
            _restaurantes = restaurante;
            _avaliacao_restaurante = avaliacao_restaurante;
        }

        /// <summary>
        /// Adiciona na colecao
        /// </summary>
        /// <param name="restaurante"></param>
        /// <returns></returns>
        public async Task Adicionar(Restaurante restaurante)
        {
            var document = _mapper.Map<RestauranteSchema>(restaurante);

            await _collection.InsertOneAsync(document);
        }

        /// <summary>
        /// Esse metodo altera todo o docmento
        /// </summary>
        /// <param name="restaurante"></param>
        /// <returns></returns>
        public async Task<bool> AtualizarCompleto(Restaurante restaurante)
        {
            try
            {
                var document = _mapper.Map<RestauranteSchema>(restaurante);
                var resultado = await Task.Run(() => _collection.ReplaceOne(e => e.Id == document.Id, document));

                return resultado.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new MongoException("Ocorreu um erro ao tentar realizar um documento no mongo", ex);
            }
        }

        /// <summary>
        /// Esse metodo altera apenas uma unica propriedade do documento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cozinha"></param>
        /// <returns></returns>
        public async Task<bool> AtualizarCozinha(string id, Cozinha cozinha)
        {
            try
            {
                var atualizacao = Builders<RestauranteSchema>.Update.Set(e => e.Cozinha, cozinha);
                var result = await _collection.UpdateOneAsync(e => e.Id == id, atualizacao);

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new MongoException("Ocorreu um erro ao atualizar a cozinha", ex);
            }
        }

        /// <summary>
        /// Esse metodo altera apenas uma unica propriedade do documento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cozinha"></param>
        /// <returns></returns>
        public async Task<bool> AtualizarHistorico(string id, IList<HistoricoRestaurante> historico)
        {
            var historicoMapper = _mapper.Map<List<HistoricoRestauranteSchema>>(historico);
            var filter = Builders<RestauranteSchema>.Filter.Eq(e => e.Id, id);
            var updateHist = Builders<RestauranteSchema>.Update.AddToSetEach(e => e.Historico, historicoMapper);

            var result = await _collection.UpdateOneAsync(filter, updateHist);
            return result.ModifiedCount > 0;
        }

        private async Task AdicionarHistorico(string id, HistoricoRestaurante historicoRestaurante)
        {
            var filter = Builders<RestauranteSchema>.Filter.And(Builders<RestauranteSchema>.Filter.Eq("_id", id));

            var update = Builders<RestauranteSchema>.Update.AddToSet($"{Restaurante}.$.Historicos", historicoRestaurante);

            await _collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<Restaurante> ObterPorId(string id)
        {
            try
            {
                var document = await Task.Run(() => _collection.AsQueryable()
                                .FirstOrDefault(e => e.Id == id));

                return _mapper.Map<Restaurante>(document);
            }
            catch (Exception ex)
            {
                throw new MongoException("Ocorreu um erro ao perquisar por id.", ex);
            }
        }

        public async Task<IEnumerable<Restaurante>> ObterTodos()
        {
            var documents = new List<RestauranteSchema>();

            //Primeira forma
            //await _collection.Find(_ => true).ForEachAsync(e =>
            //{
            //    documents.Add(e);
            //});

            //Segunda forma com Builder Filtes

            //var filter = Builders<RestauranteSchema>.Filter.Empty;
            //await _collection.Find(filter).ForEachAsync(e =>
            //{
            //    documents.Add(e);
            //});

            await _collection.AsQueryable().ForEachAsync(e =>
            {
                documents.Add(e);
            });

            return _mapper.Map<IEnumerable<Restaurante>>(documents);
        }

        public async Task<bool> BuscarHistoricoPorCozinha(string id, Cozinha cozinha)
        {
            //Busca pelo id x, que tenha no historico a cozinhay
            //var filter = Builders<RestauranteSchema>.Filter.Eq(e => e.Id, id) &
            //Builders<RestauranteSchema>.Filter.ElemMatch(e => e.Historico, e => e.Cozinha.Equals(cozinha));
            //var result = _collection.Find(filter).ToList();

            //Busca todo mundo que tenha a cozinha x dentro do historico
            var filter = Builders<RestauranteSchema>.Filter.ElemMatch(e => e.Historico,
                                                            s => s.Cozinha.Equals(cozinha));
            var result = _collection.Find(filter).ToList();

            return true;
        }

        public async Task<IEnumerable<Restaurante>> ObterPorNome(string nome)
        {
            //var filter = Builders<RestauranteSchema>.Filter.

            #region Usando AsQuerable

            //    var result = await Task.Run(() =>
            //            _collection.AsQueryable()
            //      .Where(e => e.Nome.ToUpper()
            //    .Contains(nome.ToUpper()))

            //.ToList());

            #endregion Usando AsQuerable

            var filter = new BsonDocument { { "Nome", new BsonDocument { { "$regex", nome }, { "$options", "i" } } } };
            var result = await _collection.Find(filter).ToListAsync();

            return _mapper.Map<IEnumerable<Restaurante>>(result);
        }

        /// <summary>
        /// Considerando um repositorio por entidade
        /// </summary>
        /// <param name="restauranteId"></param>
        /// <param name="avaliacao"></param>
        /// <returns></returns>
        public async Task Avaliar(string restauranteId, Avaliacao avaliacao)
        {
            await _avalicacaoRepository.IncluirAvalicacao(restauranteId, avaliacao);
        }

        /// <summary>
        /// Modo para considerar um repositorio por agregação
        /// </summary>
        /// <param name="restauranteId"></param>
        /// <param name="avaliacao"></param>
        /// <returns></returns>
        public async Task Avaliar2(string restauranteId, Avaliacao avaliacao)
        {
            var avaliacaoSchema = _mapper.Map<AvaliacaoSchema>(avaliacao);
            avaliacaoSchema.RestauranteId = restauranteId;

            await _avaliacao_restaurante
                .Collection(Avaliacao).InsertOneAsync(avaliacaoSchema);
        }
    }
}