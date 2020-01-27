using System;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public User User { get; set; }
    }
}