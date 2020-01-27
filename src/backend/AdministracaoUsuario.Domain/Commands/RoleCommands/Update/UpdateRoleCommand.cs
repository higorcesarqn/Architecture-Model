using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.RoleCommands.Update
{
    public class UpdateRoleCommand : RoleCommand, IRequest
    {
        public UpdateRoleCommand(Guid idRole ,string nome, IEnumerable<string> permissions) : base(nome, permissions)
        {
            Id = idRole;
        }
       
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new UpdateRoleCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
