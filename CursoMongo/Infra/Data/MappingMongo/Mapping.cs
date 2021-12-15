using Domain.Entities;
using Infra.Data.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento do mongo
    /// </summary>
    public static class Mapping
    {
        public static void MapearEntidadesMongo()
        {
            //verifica se já esta registrado um restaurante schema para o banco, se nao tiber registra
            if (!BsonClassMap.IsClassMapRegistered(typeof(RestauranteSchema)))
            {
                BsonClassMap.RegisterClassMap<RestauranteSchema>(rest =>
                {
                    rest.AutoMap();
                    rest.MapIdMember(e => e.Id);//mapeaando o id
                    rest.MapMember(e => e.Cozinha).SetSerializer(new EnumSerializer<Cozinha>(BsonType.Int32));//mapeado o enum para um tipo inteiro no mongo
                    rest.SetIgnoreExtraElements(true);//se algum momento estiver no banco algumas propriedades que tem no banco e nao tem no schema, ele ignora e nao da erro
                });
            }
        }
    }
}