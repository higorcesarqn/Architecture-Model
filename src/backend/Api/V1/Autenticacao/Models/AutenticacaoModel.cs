using Domain.SeedWork.Validations;
using FluentValidation;

namespace Api.V1.Autenticacao.Models
{
    public sealed class AutenticacaoModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginModelValidation : AbstractValidator<AutenticacaoModel>
    {
        public LoginModelValidation()
        {
            ValidarUserName();
            ValidarSenha();
        }

        protected void ValidarUserName()
        {
            RuleFor(x => x.Login)
                .SetValidator(new UserNameValidator("Login"));
        }

        protected void ValidarSenha()
        {
            RuleFor(x => x.Senha)
                .SetValidator(new PasswordValidator());
        }
    }
}