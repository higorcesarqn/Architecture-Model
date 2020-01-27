using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.User;
using Microsoft.AspNetCore.Http;

namespace Infra.CrossCutting.Jwt.Models
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public User(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Name => _httpContextAccessor.HttpContext.User.Identity.Name;

        public string Username
        {
            get
            {
                return GetClaimsIdentity()
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            }
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            var claimsIdentity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            return claimsIdentity?.Claims;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
