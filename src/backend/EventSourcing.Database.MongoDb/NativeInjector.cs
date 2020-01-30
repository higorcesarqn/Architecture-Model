using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Database.MongoDb
{
    public static class NativeInjector
    {
        public static IServiceCollection ConfigureEventoSourcingMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<EventStoreMongoDbContext>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            return services;
        }
    }
}
