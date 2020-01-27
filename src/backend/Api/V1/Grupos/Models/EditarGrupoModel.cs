using System.Collections.Generic;

namespace Api.V1.Grupos.Models
{
    public class EditarGrupoModel
    {
        public string Nome { get; set; }
        public IEnumerable<string> Permissoes { get; set; }
    }
}
