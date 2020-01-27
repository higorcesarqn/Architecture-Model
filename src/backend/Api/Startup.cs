using Api.Infrastructure.Filters;
using Api.Infrastructure.Helpers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ElmahCore.Mvc;
using EventSourcing.EntityFramework.PostgreSQL;
using Infra.CrossCutting.Identity;
using Infra.CrossCutting.IoC;
using Infra.CrossCutting.Jwt;
using Infra.EntityFramework.PostgreSQL;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        protected IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddApiVersioning(
               options =>
               {
                   options.ReportApiVersions = true;
               });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });


            services.AddControllers()
                .AddControllersAsServices()
                .AddMvcOptions(config =>
                {
                    //Filtros
                    config.Filters.Add(typeof(HttpGlobalExceptionFilter));
                    config.Filters.Add(typeof(ValidateModelStateFilter));

                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Point)));
                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(LineString)));
                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(MultiLineString)));
                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(MultiPolygon)));
                })
                .ConfigureFluentValidation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                    foreach (var converter in GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel())).Converters)
                    {
                        options.SerializerSettings.Converters.Add(converter);
                    }
                });

            services.AddMediatR(typeof(Startup));
            services.AddElmah(Configuration);
            services.AddSwagger();

            AddAuthentication(services);
            RegisterDbContexts(services);

           // return RegisterServices(services);
        }

        /// <summary>
        /// 
        /// </summary>
        public ILifetimeScope AutofacContainer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterServices<PostgreSqlContext>();
            //Configuração de EventSourcing
            //builder.ConfigureEventSourcing("");

        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public virtual void RegisterDbContexts(IServiceCollection services)
        {
            services.ConfigurePostgreSQLDbContext(Configuration);
            services.ConfigureEventoSourcingPostgreSqlDbContext("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public virtual void UseAuthentication(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public virtual void AddAuthentication(IServiceCollection services)
        {
            //Não Mudar a ordem.
            services.ConfigureIdentity<PostgreSqlContext>();
            services.ConfigureJwtAuthorization();

            services.AddScoped<IJwtAutenticationService, JwtAutenticationService>();
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseElmah();
            app.UseRouting();

            UseAuthentication(app);
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(provider);
        }

    }
}