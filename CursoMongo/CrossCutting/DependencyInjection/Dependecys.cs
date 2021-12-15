using Application;
using Domain.Interfaces;
using Infra.Data.Mongo;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public static class Dependecys
    {
        public static IServiceCollection ResolverDependecias(this IServiceCollection services)
        {
            ResolverApplication(services);
            ResolverRepositorys(services);

           
            return services;
        }

        /// <summary>
        /// Resolver dependencias do app
        /// </summary>
        /// <param name="services"></param>
        private static void ResolverApplication(IServiceCollection services)
        {
            services.AddScoped<IRestauranteAdicionarApplication, RestauranteCadastrarApplication>();
            services.AddScoped<IRestauranteAtualizarApplication, RestauranteAtualizarApplication>();
            services.AddScoped<IRestauranteAvaliarAppliction, RestauranteAvaliarAppliction>();
        }

        private static void ResolverRepositorys(IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IAvalicacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IRestauranteRepository, RestauranteRepository>();
            services.AddScoped<ICollectionMongoFactory<RestauranteSchema>, CollectionFactory<RestauranteSchema>>();
            services.AddScoped<ICollectionMongoFactory<AvaliacaoSchema>, CollectionFactory<AvaliacaoSchema>>();
        }
    }
}