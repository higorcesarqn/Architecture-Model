using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Entities;
using Infra.CrossCutting.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.CrossCutting.Identity.Security
{
    public class AccessManager : IAcessManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccessManager(
                UserManager<User> userManager,
                SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager
                .Users
                .Include(x => x.UserRoles).ThenInclude(x => x.Role).ThenInclude(x => x.RoleClaims)
                .FirstOrDefaultAsync(x => x.UserName == username);
            // return _userManager.FindByNameAsync(username);
        }

        public Task<SignInResult> ValidateCredentials(User user, string password)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, true);
        }
    }
}