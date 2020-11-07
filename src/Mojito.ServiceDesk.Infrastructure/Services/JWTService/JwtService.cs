using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Mojito.ServiceDesk.Infrastructure.Services.JWTService
{
    public class JwtService : IJwtService
    {
        #region ctor
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion
        public string GenerateAuthorizationToken(User user, IEnumerable<string> roles)
        {
            byte[] key = Encoding.ASCII.GetBytes(
                    configuration
                        .GetSection("AppSettings")
                        .GetSection("Secret")
                        .ToString()
                );

            var rolesObj = Newtonsoft.Json.JsonConvert.SerializeObject(roles);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, rolesObj),
                    new Claim("FullName", 
                        new StringBuilder().Append(user.FirstName)
                            .Append(" ")
                            .Append(user.LastName)
                            .ToString()),
                    new Claim("IsVerified", user.PhoneNumberConfirmed.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(3000000000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.Now.AddDays(7),
                    Created = DateTime.Now,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
