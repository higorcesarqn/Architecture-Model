using System;
using System.Text;
using Core.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infra.CrossCutting.Jwt
{
    public static class AuthorizationConfiguration
    {
        //iss (issuer) = Emissor do token;
        private const string Issuer = "egl";

        //aud (audience) = Destinatário do token, representa a aplicação que irá usá-lo.
        private const string Audience = "sit";

        private const string SecretKey = "d8fd542d-956-5d2d-8fd5-c6ffd5525874";
        private static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public static void ConfigureJwtAuthorization(this IServiceCollection services)
        {
            services.AddTransient<IUser, Models.User>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,

                // Valida a assinatura de um token recebido
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SigningKey,

                RequireExpirationTime = true,
                // Verifica se um token recebido ainda é válido
                ValidateLifetime = true,
                
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(
                o =>
                {

                    o.TokenValidationParameters = tokenValidationParameters;
                    o.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                 options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = Issuer;
                options.Audience = Audience;
                options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
            });
        }
    }
}