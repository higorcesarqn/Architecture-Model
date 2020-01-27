using System;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public User User { get; set; }
    }
}