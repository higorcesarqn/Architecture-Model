using AdministracaoUsuario.IoC;
using Autofac;
using Core.Behaviors;
using Core.Bus;
using Core.Notifications;
using Infra.CrossCutting.Bus;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices<TContext>(this ContainerBuilder container)
            where TContext : DbContext
        {

            //Configurações Gerais.
            container.ConfigureCore();
            //container.ConfigureGeo();

           
            //Configurações dos modulos.
            container.AddAdministracaoUsuario<TContext>();
        }

     

        /// <summary>
        /// Configura o Pipeline do mediator, notificações e o Bus.
        /// </summary>
        /// <param name="container"></param>
        public static void ConfigureCore(this ContainerBuilder container)
        {
            container.RegisterGeneric(typeof(CommandValidatorBehavior<,>))
               .As(typeof(IPipelineBehavior<,>))
               .InstancePerLifetimeScope();

            container.RegisterType<DomainNotificationHandler>()
                .As<INotificationHandler<DomainNotification>>()
                .InstancePerLifetimeScope();

            container.RegisterType<InMemoryBus>()
                .As<IMediatorHandler>()
                .InstancePerLifetimeScope();
        }

        private static void ConfigureGeo(this ContainerBuilder container)
        {
          //  container.RegisterType<GeometryTransformations>().InstancePerLifetimeScope();
        }

    }
}