using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EntityFramework.PostgreSQL
{
    public static class NativeInjector
    {
        public static void ConfigurePostgreSQLDbContext(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services
              .AddEntityFrameworkNpgsql()
              .AddDbContext<PostgreSqlContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("SitDbConnection"),
                sql => sql.MigrationsAssembly("Egl.Sit.Migrate")),
                contextLifetime: ServiceLifetime.Transient);
        }
    }
}
