using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Security
{
    public class RoleManager : IRoleManager
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleManager(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<IdentityResult> CreateRole(Role applicationRole)
        {
            return _roleManager.CreateAsync(applicationRole);
        }

        public Task<IdentityResult> AddClaim(Role applicationRole, Claim claim)
        {
            return _roleManager.AddClaimAsync(applicationRole, claim);
        }

        public Task<Role> GetById(Guid id)
        {
            return _roleManager.FindByIdAsync(id.ToString());
        }

        public Task<IdentityResult> Update(Role applicationRole)
        {
            return _roleManager.UpdateAsync(applicationRole);
        }

        public async Task ClearAllClaims(Role applicationRole)
        {
            var claims = await _roleManager.GetClaimsAsync(applicationRole);

            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(applicationRole, claim);
            }
        }
    }
}
