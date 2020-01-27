using System.Threading.Tasks;
using Core.Bus;
using Core.Commands;
using Core.Events;
using MediatR;

namespace Infra.CrossCutting.Bus
{
    public class InMemoryBus : Bus, IMediatorHandler
    {
        public InMemoryBus(IMediator mediator) : base(mediator)
        {
        }

        public async Task PublishCommand<T>(T command) where T : Command
        {
            await Publish(command);
        }

        public Task PublishMessage<T>(T command) where T : Message
        {
            return Publish(command);
        }

        public async Task RaiseEvent<T>(T @event) where T : Event
        {
            await Publish(@event);
        }

        public async Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> request)
        {
            return await Send(request);
        }
    }
}