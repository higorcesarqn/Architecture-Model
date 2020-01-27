using System;
using System.Collections.Generic;
using Core.Commands;

namespace AdministracaoUsuario.Domain.Commands.UserCommands
{
    public abstract class UserCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string UserName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string ConfirmPassword { get; protected set; }
        public IEnumerable<string> Roles { get; protected set; }
    }
}
