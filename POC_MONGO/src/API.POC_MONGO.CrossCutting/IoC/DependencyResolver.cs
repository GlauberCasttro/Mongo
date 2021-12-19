using API.POC_MONGO.Application;
using API.POC_MONGO.Application.Interfaces;
using API.POC_MONGO.Domain.Core.Data;
using API.POC_MONGO.Domain.Repositories;
using API.POC_MONGO.Infrastructure.Mongo;
using API.POC_MONGO.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
        }
    }
}