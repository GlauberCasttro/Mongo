using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using API.POC_MONGO.Api.Filters;
using API.POC_MONGO.Api.Logging;
using API.POC_MONGO.CrossCutting.Assemblies;
using API.POC_MONGO.CrossCutting.IoC;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using API.POC_MONGO.Infrastructure;

namespace API.POC_MONGO.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddLoggingSerilog();

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddDependencyResolver();


            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API.POC_MONGO",
                    Description = "API - API.POC_MONGO",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "API.POC_MONGO.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "API.POC_MONGO.Application.xml");

                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(applicationPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/API.POC_MONGO");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/API.POC_MONGO/swagger/v1/swagger.json", "API API.POC_MONGO");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
