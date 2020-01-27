using System.Threading;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Interfaces;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.UserCommands.Create
{
    public class CreateNewUserCommandHandler: Notifiable, IRequestHandler<CreateNewUserCommand>
    {
        private readonly IUserManager _userManager;

        public CreateNewUserCommandHandler(IUserManager userManager, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications) : base(bus, notifications)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Id, request.Name, request.Email, request.UserName);
            
            var createUserIdentityResult = await _userManager.CreateUser(user, request.Password);

            foreach (var erro in createUserIdentityResult?.Errors)
            {
                await Notify(erro.Code, erro.Description);
            }

            if (IsValid())
            {
                var addToRolesIdentityResult = await _userManager.AddToRoles(user, request.Roles);

                foreach (var erro in addToRolesIdentityResult?.Errors)
                {
                    await Notify(erro.Code, erro.Description);
                }
            }

            return Unit.Value;
        }
    }
}
