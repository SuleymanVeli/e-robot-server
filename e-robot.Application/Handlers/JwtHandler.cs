using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_robot.Application.Handlers
{
    public class JwtHandler
    {
        public static string GetJwt(IEnumerable<Claim> claims, IConfiguration configuration, dynamic obj = null)
        {
            byte[] secretBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var key = new SymmetricSecurityKey(secretBytes);
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTimeOffset.UtcNow.AddMinutes(100);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: cred
                );

            if (obj != null)
            {
                var str = obj.GetType().GetProperties();
                foreach (var k in str)
                {
                    dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);

                    jwtToken.Payload[k.Name.ToLover()] = value;
                }
            }
            var tokentHandler = new JwtSecurityTokenHandler();
            return tokentHandler.WriteToken(jwtToken);
        }
    }
}
