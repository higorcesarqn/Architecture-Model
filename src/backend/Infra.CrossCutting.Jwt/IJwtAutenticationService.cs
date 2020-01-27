using System.Threading.Tasks;
using Infra.CrossCutting.Jwt.Models;

namespace Infra.CrossCutting.Jwt
{
    public interface IJwtAutenticationService
    {
        Task<JwtToken> Authenticate(string username, string senha);
    }
}
