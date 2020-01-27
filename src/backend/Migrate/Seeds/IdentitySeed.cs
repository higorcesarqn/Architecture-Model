using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Configurations;
using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Migrate.Configuration.Identity;
using Migrate.Configuration.Permission;

namespace Migrate.Seeds
{
    public class IdentitySeed
    {
        public const string AdminRole = "Administrador";

        public static async Task EnsureSeedData<TContext>(TContext dbContext, IUserManager userManager, IRoleManager roleManager)
            where TContext : DbContext
        {
            var permissions = Permissions.GetPermissions();

            var user = new User(Guid.NewGuid(), Users.AdminUserName, Users.AdminEmail, Users.AdminUserName);

            await EnsureRoles(dbContext, roleManager, permissions);
            await EnsureAdminUser(dbContext, userManager, user);
            await EnsureRolesAdminUser(dbContext, userManager, user);
        }

        private static async Task EnsureAdminUser<TContext>(TContext dbContext, IUserManager userManager, User user)
             where TContext : DbContext
        {
            var userDbSet = dbContext.Set<User>();
            if (!await userDbSet.AnyAsync())
            {
                await userManager.CreateUser(user, Users.AdminPassword);
            }
        }

        private static async Task EnsureRolesAdminUser<TContext>(TContext dbContext, IUserManager userManager, User user)
             where TContext : DbContext
        {
            var userRoleDbSet = dbContext.Set<UserRole>();
            if(!await userRoleDbSet.AnyAsync())
            {
                await userManager.AddToRoles(user, new List<string> { AdminRole });
            }
        }

        private static async Task EnsureRoles<TContext>(TContext dbContext, IRoleManager roleManager, IEnumerable<string> permissions)
             where TContext : DbContext
        {

            var roleDbSet = dbContext.Set<Role>();

            if(!await roleDbSet.AnyAsync())
            {
                var role = new Role(Guid.NewGuid(), AdminRole);

                await roleManager.CreateRole(role);

                foreach (var permission in permissions)
                {
                    await roleManager.AddClaim(role, new Claim(IdentityConfigurations.DefaultRoleClaim, permission.ToLower()));
                }
            }
        }

    }
}
