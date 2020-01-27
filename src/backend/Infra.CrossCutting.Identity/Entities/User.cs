using System;
using System.Collections.Generic;
using Core.Aggregate;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Entities
{
    public class User : IdentityUser<Guid>, IAggregateRoot
    {
        public User(Guid id, string name, string email, string userName)
        {
            Id = id;
            Name = name;
            Email = email;
            UserName = userName;
        }

        protected User() { }

        public string Name { get; set; }

        public ICollection<UserClaim> UserClaims { get; set; }
        public ICollection<UserLogin> UserLogins { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}