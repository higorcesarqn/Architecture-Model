using System.Threading.Tasks;
using Core.Events;
using MediatR;

namespace Infra.CrossCutting.Bus
{
    public abstract class Bus
    {
        private readonly IMediator _mediator;

        protected Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected Task Publish<TEvent>(TEvent message) where TEvent : Message
        {
            return _mediator.Publish(message);
        }

        protected Task<TResponse> Send<TResponse>(IRequest<TResponse> command)
        {
            return _mediator.Send(command);
        }
    }
}