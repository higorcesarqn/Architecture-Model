using System;
using Domain.SeedWork.Validations;
using FluentValidation;

namespace AdministracaoUsuario.Domain.Commands.UserCommands
{
    public abstract class UserCommandValidator<TUserCommand> : AbstractValidator<TUserCommand> 
        where TUserCommand : UserCommand
    {
        private const int TAMANHO_MINIMO_NOME = 3;

        protected void IdValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(default(Guid))
                .WithMessage("O Usuário é Inválido.");
        }

        protected void NameValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O Nome é Obrigatório.");

            RuleFor(x => x.Name)
                .MinimumLength(TAMANHO_MINIMO_NOME)
                .WithMessage($"O Nome tem que ter {TAMANHO_MINIMO_NOME} ou mais caracteres.");
        }

        protected void UsernameValidator()
        {
            RuleFor(x => x.UserName)
                .SetValidator(new UserNameValidator("Nome de Usuário"));
        }

        protected void PasswordValidator()
        {
            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator()) ;
        }

        protected void ConfirmPasswordValidator()
        {
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
                .WithMessage("Confirma Senha não confere com a Senha digitada.");
        }

        protected void EmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O Email é obrigatorio");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("O Email informado não é válido.");
        }
    }
}
