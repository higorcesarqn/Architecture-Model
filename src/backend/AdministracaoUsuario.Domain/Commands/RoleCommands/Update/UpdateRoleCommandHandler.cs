using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Infra.CrossCutting.Identity.Configurations;
using Infra.CrossCutting.Identity.Interfaces;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.RoleCommands.Update
{
    public class UpdateRoleCommandHandler : Notifiable, IRequestHandler<UpdateRoleCommand>
    {
        private readonly IRoleManager _roleManager;

        public UpdateRoleCommandHandler(IRoleManager roleManager, IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(bus, notifications)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.GetById(request.Id);

            if (role == null)
            {
                await Notify("grupo", "grupo não encontrada.");
                return Unit.Value;
            }

            role.Name = request.Name;

            var createRoleIdentityResult = await _roleManager.Update(role);

            foreach (var erro in createRoleIdentityResult.Errors)
            {
                await Notify(erro.Code, erro.Description);
            }

            if (IsValid())
            {
                await _roleManager.ClearAllClaims(role);
                foreach (var permissao in request.Permissions)
                {
                    var addClaim = await _roleManager
                        .AddClaim(role, new Claim(IdentityConfigurations.DefaultRoleClaim, permissao.ToLower()));
                }
            }

            return Unit.Value;
        }
    }
}
