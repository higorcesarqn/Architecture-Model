using System.Threading;
using System.Threading.Tasks;
using Core.Bus;
using Core.Commands;
using Core.Notifications;
using MediatR;

namespace Core.Behaviors
{
    public class CommandValidatorBehavior<TRequest, TResponse> : Notifiable, IPipelineBehavior<TRequest, TResponse>
    {
        public CommandValidatorBehavior(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
            : base(bus, notifications) { }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //se o request for um Command e estiver inválido, é notificado os erros.
            if (request is Command command && !await command.IsValid())
            {
                await NotifyValidationErrors(command);
                return default;
            }

            return await next();
        }
    }
}