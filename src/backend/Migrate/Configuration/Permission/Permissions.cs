using System.Collections.Generic;

namespace Migrate.Configuration.Permission
{
    public static class Permissions
    {
        public static IEnumerable<string> GetPermissions()
        {
            yield return "usuarios-criar";
            yield return "usuarios-editar";
            yield return "usuarios-excluir";
            yield return "usuarios-listar";
            yield return "grupos-criar";
            yield return "grupos-editar";
            yield return "grupos-excluir";
            yield return "grupos-listar";
            yield return "pessoas-criar";
            yield return "pessoas-editar";
            yield return "pessoas-excluir";
            yield return "pessoas-listar";
        }
    }
}
