using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EntityFramework.PostgreSQL
{
    public static class NativeInjector
    {
        public static void ConfigurePostgreSQLDbContext(this IServiceCollection services, string connectionString)
        {
            services
              .AddEntityFrameworkNpgsql()
              .AddDbContext<PostgreSqlContext>(
                options => options.UseNpgsql(connectionString,
                sql => sql.MigrationsAssembly("Migrate")),
                contextLifetime: ServiceLifetime.Transient);
        }
    }
}
