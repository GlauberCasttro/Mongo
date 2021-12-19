using API.POC_MONGO.Domain.Entities;
using API.POC_MONGO.Infrastructure.Schemas;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace API.POC_MONGO.Infrastructure.Mongo.Mappings
{
    public static class MappingSchema
    {
        public static void MapearEntidadesMongo()
        {
            //verifica se já esta registrado um restaurante schema para o banco, se nao tiber registra
            if (!BsonClassMap.IsClassMapRegistered(typeof(ClienteSchema)))
            {
                BsonClassMap.RegisterClassMap<ClienteSchema>(rest =>
                {
                    rest.AutoMap();
                    rest.MapIdMember(e => e.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId)); //mapeaando o id
                    rest.MapMember(e => e.Situacao).SetSerializer(new EnumSerializer<Situacao>(BsonType.Int32));//mapeado o enum para um tipo inteiro no mongo
                    rest.SetIgnoreExtraElements(true);//se algum momento estiver no banco algumas propriedades que tem no banco e nao tem no schema, ele ignora e nao da erro
                    rest.GetMemberMap(c => c.DataCriacao).SetElementName("dt_criacao");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(HistoricoClienteSchema)))
            {
                BsonClassMap.RegisterClassMap<HistoricoClienteSchema>(rest =>
                {
                    rest.AutoMap();
                    rest.MapIdMember(e => e.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));
                    rest.GetMemberMap(c => c.DataAlteracacao).SetElementName("dt_criacao");
                    rest.GetMemberMap(c => c.NomeCliente).SetElementName("nome");
                });
            }
        }
    }
}