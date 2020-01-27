using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Security
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> _userManager;

        public UserManager(UserManager<User> userManager)
        {
            
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateUser(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> ChangePassword(User user, string password)
        {
            var hash = _userManager.PasswordHasher.HashPassword(user,password);
            user.PasswordHash = hash;
            return Update(user);
        }

        public Task<IdentityResult> AddToRoles(User user, IEnumerable<string> roles)
        {
            return _userManager.AddToRolesAsync(user, roles);
        }

        public Task<IdentityResult> Update(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task<User> GetById(Guid idUser)
        {

            return _userManager.FindByIdAsync(idUser.ToString());
        }

        public Task<IdentityResult> RemoveRoles(User user, IEnumerable<string> roles)
        {
            return _userManager.RemoveFromRolesAsync(user, roles);
        }

        public Task<IList<string>> GetRoles(User user)
        {
            return _userManager.GetRolesAsync(user);
        }
    }
}
