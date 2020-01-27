using ElmahCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Helpers
{
    /// <summary>
    /// extencions elmah
    /// </summary>
    public static class ConfigureElmah
    {
        /// <summary>
        /// Adicionar o Elmah na aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddElmah(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddElmah(
            //services.AddElmah<PgsqlErrorLog>(
                options =>
                {
                    options.ApplicationName = "Caesar";
                   // options.ConnectionString = configuration.GetConnectionString("SitDbConnection");
                   // options.CheckPermissionAction = context => context.User.Identity.IsAuthenticated;
                });

            return services;
        }
    }
}
