using System.Threading.Tasks;
using Core.Commands;
using Core.Events;
using MediatR;

namespace Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishCommand<T>(T command) where T : Command;

        Task PublishMessage<T>(T message) where T : Message;

        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command);

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}