using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Interfaces
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> ChangePassword(User user, string password);
        Task<IdentityResult> AddToRoles(User user, IEnumerable<string> roles);
        Task<User> GetById(Guid idUser);
        Task<IdentityResult> Update(User user);
        Task<IdentityResult> RemoveRoles(User user, IEnumerable<string> roles);
        Task<IList<string>> GetRoles(User user);
    }
}
