using Egl.Sit.Api.Configuration.Test;
using Egl.Sit.Api.IntegrationTests.Common;
using Egl.Sit.Infra.CrossCutting.Jwt;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Xunit;

namespace Egl.Sit.Api.IntegrationTests.Tests.Base
{
    public class BaseClassFixture : IClassFixture<WebApplicationFactory<StartupTest>>
    {
        protected readonly WebApplicationFactory<StartupTest> Factory;
        protected readonly HttpClient Client;

        public BaseClassFixture(WebApplicationFactory<StartupTest> factory)
        {
            Factory = factory;
            Client = factory.SetupClient();
            //Factory.CreateClient();
           
        }

        protected virtual void SetupAdminClaimsViaHeaders()
        {
            using var scope = Factory.Services.CreateScope();
            var tokenOptions = scope.ServiceProvider.GetRequiredService<IOptions<TokenOptions>>().Value;

            var jwtSecurityToken = new JwtSecurityToken(
                            tokenOptions.Issuer,
                            tokenOptions.Audience,
                            GetClaims(tokenOptions),
                            tokenOptions.NotBefore,
                            tokenOptions.Expiration,
                            tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            Client.DefaultRequestHeaders.Add("Authorization", $"bearer {encodedJwt}");
        }

        private static IEnumerable<Claim> GetClaims(TokenOptions tokenOptions)
        {
            yield return new Claim(JwtRegisteredClaimNames.UniqueName, "usuario teste");
            yield return new Claim(JwtRegisteredClaimNames.Email, "usuario_teste@sit.egl.com");
            yield return new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString());
            yield return new Claim(JwtRegisteredClaimNames.Jti, tokenOptions.JtiGenerator());
            yield return new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64);
            
            yield return new Claim(ClaimTypes.Role, "pessoas-adicionar");
            yield return new Claim(ClaimTypes.Role, "pessoas-editar");
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
