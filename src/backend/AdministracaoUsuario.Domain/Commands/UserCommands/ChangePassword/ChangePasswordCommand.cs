using System;
using System.Threading.Tasks;
using MediatR;

namespace AdministracaoUsuario.Domain.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommand : UserCommand, IRequest
    {
        public ChangePasswordCommand(Guid idUser, string password, string confirmPassword)
        {
            Id = idUser;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ChangePasswordCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
