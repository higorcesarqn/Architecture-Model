using FluentValidation;

namespace BusinessLogic.Shared.ValuesObjects.Validations
{
    public class EmailAddressValidator : AbstractValidator<string>
    {
        public EmailAddressValidator()
        {
            RuleFor(x => x)
               .NotEmpty()
               .WithMessage("O E-mail é Obrigatório.")
               .EmailAddress()
               .WithMessage("Não é um email Válido.");
        }
    }
}
