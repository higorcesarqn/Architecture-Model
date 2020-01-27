using System;
using System.Collections.Generic;

namespace Infra.CrossCutting.Jwt.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Permissoes { get; set; }
    }
}