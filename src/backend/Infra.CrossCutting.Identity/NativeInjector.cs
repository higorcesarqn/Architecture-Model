using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Infrastructure;
using Infra.CrossCutting.Identity.Interfaces;
using Infra.CrossCutting.Identity.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Identity
{
    public static class NativeInjector
    {
        public static void ConfigureIdentity<TContext>(this IServiceCollection services)
           where TContext : DbContext
        {
            services.AddScoped<IAcessManager, AccessManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();

            services.ConfigureIdentityOptions();
        }
    }
}
