using Autofac;
using Autofac.Extensions.DependencyInjection;
using Egl.Core.Bus;
using Egl.Sit.EventSourcing.EntityFramework.InMemory;
using Egl.Sit.EventSourcing.EntityFramework.PostgreSQL;
using Egl.Sit.EventSourcing.IoC;
using Egl.Sit.EventSourcing.UnitTests.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Egl.Sit.Infra.CrossCutting.IoC;
using Xunit;

namespace Egl.Sit.EventSourcing.UnitTests
{
    public class EventSourcingTest
    {
        private readonly IServiceProvider _serviceProvider;

        public EventSourcingTest()
        {
            _serviceProvider = GetServices();
        }

        [Fact]
        [Trait("Event Sourcing", "Test")]
        public async Task Test_EventSourcing()
        {
            var command = new CreateNewUserCommand(Guid.Empty, "Higor César", "higorcesar@email.com",
                new DateTime(1985, 01, 22), "higorcesar");

            var bus = _serviceProvider.GetService<IMediatorHandler>();

            await bus.SendCommand(command);

            var repository = _serviceProvider.GetService<IEventStoreRepository>();

            var result = await repository.All(command.Id);
            Debug.Assert(result != null, $"{nameof(result)} != null");
            var obj = JsonConvert.DeserializeObject<CreateNewUserCommand>(result.FirstOrDefault().Data);

            Assert.Equal(command.Id, obj.AggregateId);
            Assert.Equal(command.Name, obj.Name);
            Assert.Equal(command.Email, obj.Email);
            Assert.Equal(command.Birthday, obj.Birthday);

        }

        private static IServiceProvider GetServices()
        {
            var services = new ServiceCollection();
            var container = new ContainerBuilder();

            container.ConfigureCore();

            container.RegisterType<CreateNewUserCommandHandler>().As<IRequestHandler<CreateNewUserCommand>>();

            container.AddEventSourcing<EventStoreRepository<EventSoucingInMemoryContext>>();

            container.RegisterType<User>().AsImplementedInterfaces().SingleInstance();

            services.AddDbContext<EventSoucingInMemoryContext>(contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);

            services.AddMediatR(typeof(EventSourcingTest));

            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }
    }

}