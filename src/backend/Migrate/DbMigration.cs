using System;
using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Interfaces;
using Infra.EntityFramework.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrate.Seeds;

namespace Migrate
{
    public static class DbMigration
    {
        public static async Task EnsureSeedData(IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            await EnsureDatabasesMigrated<PostgreSqlContext>(services);
            await EnsureSeedData<PostgreSqlContext>(services);
        }

        /**
        Add-Migration DbInit -context PostgreSQLContext -output Data/Migrations/PostgreSQL
        **/
        private static async Task EnsureDatabasesMigrated<TDbContext>(IServiceProvider services)
            where TDbContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            await using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await context.Database.MigrateAsync();
        }

        private static async Task EnsureSeedData<TDbContext>(IServiceProvider services)
            where TDbContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
          
            //Identity Seed
            var userManager = scope.ServiceProvider.GetRequiredService<IUserManager>();
            var roleManager = scope.ServiceProvider.GetRequiredService<IRoleManager>();
            var usuarioDbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await IdentitySeed.EnsureSeedData(usuarioDbContext, userManager, roleManager);
        }
    }
}
