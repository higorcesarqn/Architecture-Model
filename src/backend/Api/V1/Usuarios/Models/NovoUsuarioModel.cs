using System.Collections.Generic;

namespace Api.V1.Usuarios.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AdicionarUsuarioModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConfirmaSenha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Grupos { get; set; }
    }
}
