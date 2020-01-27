using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Humanizer;
using Infra.CrossCutting.Identity.Interfaces;
using Infra.CrossCutting.Jwt.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Tango.Types;

#nullable enable

namespace Infra.CrossCutting.Jwt
{
    public class JwtAutenticationService : Notifiable, IJwtAutenticationService
    {
        private readonly IAcessManager _userManager;
        private readonly TokenOptions _tokenOptions;

        public JwtAutenticationService(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications,
            IAcessManager userManager, IOptions<TokenOptions> jwtOptions) : base(bus, notifications)
        {
            _userManager = userManager;
            _tokenOptions = jwtOptions.Value;
        }

        private async Task<JwtToken?> MethoWhenSignFail()
        {
            await Notify("usuario", "Usuário e/ou Senha Inválido(s)");
            return default;
        }

        public async Task<JwtToken> Authenticate(string username, string senha)
        {
            Option<Identity.Entities.User> userIdentity = await _userManager.GetUserByUsername(username);

            return await userIdentity.Match(user => MethodWhenUserExist(user, senha), MethoWhenSignFail);
        }

        private async Task<JwtToken?> MethodWhenUserExist(Identity.Entities.User user, string senha)
        {
            var signInResult = await _userManager.ValidateCredentials(user, senha);

            if (signInResult.Succeeded)
            {
                var claims = GetClaims(user);

                var jwtSecurityToken = new JwtSecurityToken(
                            _tokenOptions.Issuer,
                            _tokenOptions.Audience,
                            claims,
                            _tokenOptions.NotBefore,
                            _tokenOptions.Expiration,
                            _tokenOptions.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return new JwtToken
                {
                    Token = encodedJwt,
                    Expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                    User = new UserModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        Username = user.UserName,
                        Permissoes = user.UserRoles
                            .SelectMany(x => x.Role.RoleClaims
                            .Select(s => s.ClaimValue))
                    }
                };
            }

            if (signInResult.IsLockedOut)
            {
                var timespan = user.LockoutEnd - DateTime.Now;
                await Notify("usuario", $"numero máximo de tentativas excedidas! tente novamente em {timespan?.Humanize(2)}");
                return default;
            }

            await MethoWhenSignFail();

            return default;
        }

        public IEnumerable<Claim> GetClaims(Identity.Entities.User user)
        {
            if (user == default)
            {
                yield break;
            }

            yield return new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName);
            yield return new Claim(JwtRegisteredClaimNames.Email, user.Email);
            yield return new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
            yield return new Claim(JwtRegisteredClaimNames.Jti, _tokenOptions.JtiGenerator());
            yield return new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64);

            if (user.UserRoles == null)
            {
                yield break;
            }

            foreach (var role in user.UserRoles.SelectMany(x => x.Role?.RoleClaims))
            {
                yield return new Claim(ClaimTypes.Role, role.ClaimValue);
            }
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}