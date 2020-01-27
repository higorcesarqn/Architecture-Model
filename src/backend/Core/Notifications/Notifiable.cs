using System.Threading.Tasks;
using Core.Bus;
using Core.Commands;
using FluentValidation.Results;
using MediatR;

namespace Core.Notifications
{
    public abstract class Notifiable
    {
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        protected Notifiable(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected virtual async Task ValidateAndNotifyValidationErrors(Command message)
        {
            await message.IsValid();
            await NotifyValidationErrors(message);
        }

        protected virtual Task NotifyValidationErrors(Command message)
        {
            return NotifyValidationErrors(message.ValidationResult);
        }

        protected virtual async Task NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await Notify(error.PropertyName, error.ErrorMessage);
            }
        }

        protected virtual Task Notify(string key, string value)
        {
            return Notify(new DomainNotification(key, value));
        }

        protected virtual Task Notify(DomainNotification notification)
        {
            return _bus.RaiseEvent(notification);
        }

        protected virtual bool IsValid()
        {
            return !_notifications.HasNotifications();
        }
    }
}