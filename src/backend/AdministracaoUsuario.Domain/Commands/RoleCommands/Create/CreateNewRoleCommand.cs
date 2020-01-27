using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.RoleCommands.Create
{
    public class CreateNewRoleCommand : RoleCommand, IRequest
    {
        public CreateNewRoleCommand(string nome, IEnumerable<string> permissions) : base(nome, permissions) { }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new CreateNewRoleCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
