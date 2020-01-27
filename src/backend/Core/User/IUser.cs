using System.Collections.Generic;
using System.Security.Claims;

namespace Core.User
{
    public interface IUser
    {
        string Name { get; }
        string Username { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}
