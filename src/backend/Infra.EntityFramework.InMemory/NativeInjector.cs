using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EntityFramework.InMemory
{
    public static class NativeInjector
    {
        public static void ConfigureInMemoryDbContext(this IServiceCollection services)
        {
            services
              .AddDbContext<SitInMemoryContext>(
                options =>  options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        }
    }
}
