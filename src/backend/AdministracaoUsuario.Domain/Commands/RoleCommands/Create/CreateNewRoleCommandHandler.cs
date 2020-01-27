using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Infra.CrossCutting.Identity.Configurations;
using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Interfaces;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.RoleCommands.Create
{
    public class CreateNewRoleCommandHandler : Notifiable, IRequestHandler<CreateNewRoleCommand>
    {
        private readonly IRoleManager _roleManager;

        public CreateNewRoleCommandHandler(IRoleManager roleManager, IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(bus, notifications)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(CreateNewRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Id, request.Name);

            var createRoleIdentityResult = await _roleManager.CreateRole(role);

            foreach (var erro in createRoleIdentityResult?.Errors)
            {
                await Notify(erro.Code, erro.Description);
            }

            if (IsValid())
            {
                foreach (var permissao in request.Permissions)
                {
                    _ = await _roleManager
                        .AddClaim(role, new Claim(IdentityConfigurations.DefaultRoleClaim, permissao.ToLower()));
                }
            }

            return Unit.Value;
        }
    }
}
