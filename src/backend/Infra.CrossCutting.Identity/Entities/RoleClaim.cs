using System;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public Role Role { get; set; }
    }
}