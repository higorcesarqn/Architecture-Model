namespace AdministracaoUsuario.Domain.Commands.RoleCommands.Update
{
    public class UpdateRoleCommandValidator : RoleCommandValidations<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            NameValidator();
            PermissionsValidator();
            IdValidator();
        }
    }
}
