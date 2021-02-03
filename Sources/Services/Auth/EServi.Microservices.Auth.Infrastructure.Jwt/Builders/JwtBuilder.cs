using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EServi.Microservices.Auth.Infrastructure.Jwt.Builders
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly JwtOptions _options;

        public JwtBuilder(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(Dictionary<string, string> values)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretId));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes);

            var claims = new ClaimsIdentity();

            foreach (var (key, value) in values)
            {
                claims.AddClaim(new Claim(key, value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expirationDate,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }

        public string ValidateAuthToken(string token)
        {
            var principal = GetPrincipal(token);

            var identity = (ClaimsIdentity) principal?.Identity;

            if (identity == null)
            {
                return string.Empty;
            }

            var userIdClaim = identity.FindFirst("userId");

            return userIdClaim.Value;
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = (JwtSecurityToken) tokenHandler.ReadToken(token);

                if (jwtToken == null)
                {
                    return null;
                }

                var key = Encoding.UTF8.GetBytes(_options.SecretId);

                var parameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                IdentityModelEventSource.ShowPII = true;

                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}