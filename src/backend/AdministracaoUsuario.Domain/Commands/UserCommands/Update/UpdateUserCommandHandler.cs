using System.Threading;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Infra.CrossCutting.Identity.Interfaces;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.UserCommands.Update
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUpdateUserCommand">UserCommand, IRequest</typeparam>
    public class UpdateUserCommandHandler : Notifiable, IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserManager _userManager;

        public UpdateUserCommandHandler(IUserManager userManager, IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(bus, notifications)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.GetById(request.Id);

            if (applicationUser == null)
            {
                await Notify("usuario", "Usuário não existe!");
                return Unit.Value;
            }

            applicationUser.Name = request.Name;
            applicationUser.Email = request.Email;
            var updateResult = await _userManager.Update(applicationUser);

            if (updateResult.Succeeded)
            {
                var roles = await _userManager.GetRoles(applicationUser);
                await _userManager.RemoveRoles(applicationUser, roles);
                var addRoles = await _userManager.AddToRoles(applicationUser, request.Roles);

                foreach (var erro in addRoles.Errors)
                {
                    await Notify(erro.Code, erro.Description);
                }
            }

            foreach (var erro in updateResult.Errors)
            {
                await Notify(erro.Code, erro.Description);
            }

            return Unit.Value;
        }
    }
}
