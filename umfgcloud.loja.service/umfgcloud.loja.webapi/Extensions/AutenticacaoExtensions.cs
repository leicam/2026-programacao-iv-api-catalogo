using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;
using umfgcloud.loja.dominio.service.Classes;

namespace umfgcloud.loja.webapi.Extensions
{
    internal static class AutenticacaoExtensions
    {
        internal static void AddAutenticacao(this IServiceCollection services,
            IConfiguration configuration)
        {
            var confirurationSectionJwtOptions = configuration.GetSection(nameof(JwtOptions)).GetChildren();

            var issuer = confirurationSectionJwtOptions
                .FirstOrDefault(x => x.Key == nameof(JwtOptions.Issuer))?.Value ?? string.Empty;
            var audiance = confirurationSectionJwtOptions
                .FirstOrDefault(x => x.Key == nameof(JwtOptions.Audience))?.Value ?? string.Empty;
            var securityKey = confirurationSectionJwtOptions
                .FirstOrDefault(x => x.Key == nameof(JwtOptions.SecurityKey))?.Value ?? string.Empty;

            var symmetricSecurityKey =  new SymmetricSecurityKey(Convert.FromBase64String(securityKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audiance,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audiance;

                options.AcessTokenExpiration = int.Parse(
                    confirurationSectionJwtOptions
                    .FirstOrDefault(x => x.Key == nameof(JwtOptions.AcessTokenExpiration))?.Value ?? string.Empty);

                options.RefreshTokenExpiration = int.Parse(
                    confirurationSectionJwtOptions
                    .FirstOrDefault(x => x.Key == nameof(JwtOptions.RefreshTokenExpiration))?.Value ?? string.Empty);

                options.SigningCredentials = signingCredentials;
            });

            //define quais as caracteristicas de uma senha
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            });

            Debug.WriteLine($"key configurando o token: {signingCredentials.Key}");

            //definir a autenticacao via JWT
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => 
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}