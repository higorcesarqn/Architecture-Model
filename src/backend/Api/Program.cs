using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Migrate;
using Serilog;

namespace Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private const string SeedArgs = "/seed";

        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(Configuration)
              .Enrich.FromLogContext()
              .CreateLogger();

            var seed = args.Any(x => x == SeedArgs);
            var envSeed = Environment.GetEnvironmentVariable("SIT_SEED")?.ToLower();
            seed = seed || envSeed == "true";

            if (seed) args = args.Except(new[] { SeedArgs }).ToArray();

            try
            {
                Log.Information("Inicializando a API!");
                var host = CreateHostBuilder(args)
                   
                    .Build();

                //dotnet run /seed
                if (seed)
                {
                    await DbMigration.EnsureSeedData(host);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Erro ao inicializar!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });

            
    }
}