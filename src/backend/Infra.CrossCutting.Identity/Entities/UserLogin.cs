using System;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public User User { get; set; }
    }
}