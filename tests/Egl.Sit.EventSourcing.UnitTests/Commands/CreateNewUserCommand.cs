using System;
using System.Threading.Tasks;
using Egl.Core.Commands;
using MediatR;

namespace Egl.Sit.EventSourcing.UnitTests.Commands
{
    public class CreateNewUserCommand : Command, IRequest
    {
        public CreateNewUserCommand(Guid id, string name, string email, DateTime birthday, string username)
        {
            Id = id == default ? Guid.NewGuid() : id;
            AggregateId = Id;
            Name = name;
            Email = email;
            Birthday = birthday;
            Username = username;
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public DateTime Birthday { get; protected set; }
        public string Username { get; protected set; }


        public override async Task<bool> IsValid()
        {
            return true;
        }
    }
}