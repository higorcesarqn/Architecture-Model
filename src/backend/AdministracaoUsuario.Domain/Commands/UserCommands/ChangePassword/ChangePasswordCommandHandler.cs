using System.Threading;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Infra.CrossCutting.Identity.Interfaces;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommandHandler : Notifiable, IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserManager _userManager;

        public ChangePasswordCommandHandler(IUserManager userManager, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications) : base(bus, notifications)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.GetById(request.Id);

            if (applicationUser == null)
            {
                await Notify("usuario", "usuario inválido.");
                return Unit.Value;
            }

            var changePasswordResult = await _userManager.ChangePassword(applicationUser, request.Password);

            foreach (var erro in changePasswordResult.Errors)
            {
                await Notify(erro.Code, erro.Description);
            }

            return Unit.Value;
        }
    }
}
