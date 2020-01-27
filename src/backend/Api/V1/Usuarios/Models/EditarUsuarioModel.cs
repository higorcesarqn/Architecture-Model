using System.Collections.Generic;

namespace Api.V1.Usuarios.Models
{
    public class EditarUsuarioModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Grupos { get; set; }
    }
}
