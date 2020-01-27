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
        Add-Migration SitDbInit -context SitPostgreSQLContext -output Data/Migrations/SitDb
        Add-Migration EventSourcingDbInit -context EventStoreEventSourcingContext -output Data/Migrations/EventSourcingDb
        **/
        private static async Task EnsureDatabasesMigrated<TDbContext>(IServiceProvider services)
            where TDbContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            await using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await context.Database.MigrateAsync();
        }

        private static async Task EnsureSeedData<TSitContext>(IServiceProvider services)
            where TSitContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
          
            //Identity Seed
            var userManager = scope.ServiceProvider.GetRequiredService<IUserManager>();
            var roleManager = scope.ServiceProvider.GetRequiredService<IRoleManager>();
            var usuarioDbContext = scope.ServiceProvider.GetRequiredService<TSitContext>();
            await IdentitySeed.EnsureSeedData(usuarioDbContext, userManager, roleManager);
        }
    }
}
