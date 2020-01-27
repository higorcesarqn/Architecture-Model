using System;
using FluentValidation;

namespace AdministracaoUsuario.Domain.Commands.RoleCommands
{
    public class RoleCommandValidations<T> : AbstractValidator<T> where T : RoleCommand
    {
        public void NameValidator()
        {
            RuleFor(x => x.Name)
                
                .NotEmpty().WithMessage("O Nome é Obrigatorio.").OverridePropertyName("Nome");

            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("O Comprimento do nome tem que ter 5 ou mais caracteres.").OverridePropertyName("Nome"); ;
        }

        public void PermissionsValidator()
        {
            RuleFor(x => x.Permissions)
                .NotEmpty()
                .WithMessage("Tem que ter no minimo uma permissão para criar um grupo.").OverridePropertyName("Permissoes");
        }

        protected void IdValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(new Guid())
                .WithMessage("Grupo Inválido.");
        }
    }
}
