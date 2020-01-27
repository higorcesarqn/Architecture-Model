using Autofac;
using Egl.Core.Events;
using Egl.Sit.EventSourcing.EntityFramework.PostgreSQL;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Egl.Sit.EventSourcing.IoC
{
    public static class NativeInjector
    {
        public static void ConfigureEventSourcing<TContext>(this ContainerBuilder container)
            where TContext : DbContext
        {
            
            container.AddEventSourcing<EventStoreRepository<TContext>>();
        }

        public static void AddEventSourcing<TRepository>(this ContainerBuilder container)
            where TRepository : IEventStoreRepository
        {
            container.RegisterType<TRepository>()
                .As<IEventStoreRepository>()
                .InstancePerLifetimeScope();

            container.RegisterType<EventStore>()
                .As<IEventStore>()
                .InstancePerLifetimeScope();

            container.RegisterGeneric(typeof(EventoStoreEventoHandler<,>))
                .As(typeof(IRequestPostProcessor<,>))
                .InstancePerLifetimeScope();
        }
    }
}
