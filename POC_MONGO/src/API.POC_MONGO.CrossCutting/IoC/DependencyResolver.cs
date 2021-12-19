using API.POC_MONGO.Application;
using API.POC_MONGO.Application.Interfaces;
using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure;
using API.POC_MONGO.Infrastructure.Mongo;
using API.POC_MONGO.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Diagnostics.CodeAnalysis;

namespace API.POC_MONGO.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterApplications(services);
            RegisterRepositories(services);
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<IClienteApplication, ClienteApplication>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            RegisterMongo(services);

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteHistoricoRepository, ClienteHistoricoRepository>();
        }

        private static void RegisterMongo(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(c =>
            {
                var settings = MongoClientSettings.FromConnectionString(Config.Mongo.ConnectionString);
                settings.WaitQueueTimeout = TimeSpan.FromMinutes(15);
                settings.MinConnectionPoolSize = 100;
                settings.MaxConnectionPoolSize = 500;
                return new MongoClient(settings);
            });

            services.AddScoped(c => c.GetService<IMongoClient>().StartSession());
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<ITransaction, Transaction>();
        }
    }
}