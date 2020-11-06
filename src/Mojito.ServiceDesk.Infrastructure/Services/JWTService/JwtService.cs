using Microsoft.IdentityModel.Tokens;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mojito.ServiceDesk.Infrastructure.Services.JWTService
{
    public class JwtService : IJwtService
    {
        public string GenerateAuthorizationToken(User user, IEnumerable<string> roles)
        {
            byte[] key = Encoding.ASCII.GetBytes("SECRET_KEY22222222222222222222222");

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
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
