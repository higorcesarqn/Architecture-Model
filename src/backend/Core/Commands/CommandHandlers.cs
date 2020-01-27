using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Core.UnitOfWork;
using MediatR;

namespace Core.Commands
{
    public abstract class CommandHandler : Notifiable
    {
        private readonly IUnitOfWork _uow;

        protected CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
            : base(bus, notifications)
        {
            _uow = uow;
        }

        protected Task BeginTransaction()
        {
            return _uow.BeginTransactionAsync();
        }

        protected virtual async Task SaveChanges()
        {
            if (IsValid())
            {
                var result = await _uow.SaveChangesAsync();
                if(result == 0)
                {
                    await Notify("salvar", "Tivemos um problema ao salvar");
                }
            }
        }

        protected virtual Task CommitTransation()
        {
            return IsValid() ? _uow.SaveChangesAsync() : Task.CompletedTask;
        }

        protected virtual void Rollback()
        {
            _uow.RollbackTransaction();
        }
    }
}