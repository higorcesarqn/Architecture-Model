using System.Threading;
using System.Threading.Tasks;
using Core.Events;
using MediatR.Pipeline;

namespace EventSourcing
{
    public class EventoStoreEventoHandler<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly IEventStore _eventStore;

        public EventoStoreEventoHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (request is Event @event)
            {
                await _eventStore.Save(@event);
            }
        }
    }
}
