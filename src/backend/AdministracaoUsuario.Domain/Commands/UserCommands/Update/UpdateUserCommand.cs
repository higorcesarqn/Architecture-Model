using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.UserCommands.Update
{
    public class UpdateUserCommand : UserCommand, IRequest
    {
        public UpdateUserCommand(Guid idUsuario, string name, string email, IEnumerable<string> roles)
        {
            Id = idUsuario;
            Name = name;
            Email = email;
            Roles = roles;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new UpdateUserCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
