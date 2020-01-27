using System.Threading.Tasks;
using Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infra.CrossCutting.Identity.Interfaces
{
    public interface IAcessManager
    {
        Task<User> GetUserByUsername(string username);
        Task<SignInResult> ValidateCredentials(User user, string password);
    }
}