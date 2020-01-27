namespace AdministracaoUsuario.Domain.Commands.RoleCommands.Create
{
    public class CreateNewRoleCommandValidation : RoleCommandValidations<CreateNewRoleCommand>
    {
        public CreateNewRoleCommandValidation()
        {
            NameValidator();
            PermissionsValidator();
        }
    }
}
