using Core.Events;
using MediatR.Pipeline;
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
            services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(EventoStoreEventoHandler<,>));
            services.AddScoped<IEventStore, EventStore>();
            return services;
        }
    }
}
