using System;
using System.Collections.Generic;
using Core.Aggregate;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Entities
{
    public class Role : IdentityRole<Guid>, IAggregateRoot
    {
        public Role(Guid id,string name)
        {
            Id = id;
            Name = name;
        }

        protected Role() { }

        public ICollection<RoleClaim> RoleClaims { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}