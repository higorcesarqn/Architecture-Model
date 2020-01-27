using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.UserCommands.Create
{
    public class CreateNewUserCommand : UserCommand, IRequest
    {
        public CreateNewUserCommand(string nome, string login, string email, string senha, string confirmaSenha, IEnumerable<string> roles)
        {
            Id = Guid.NewGuid();
            Name = nome;
            UserName = login;
            Email = email;
            Password = senha;
            ConfirmPassword = confirmaSenha;
            Roles = roles ?? Enumerable.Empty<string>();
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new CreateNewUserCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
