namespace AdministracaoUsuario.Domain.Commands.UserCommands.Update
{
    public class UpdateUserCommandValidator : UserCommandValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            IdValidator();
            EmailValidator();
            NameValidator();
        }
    }
}
