using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.contracts;
using Dominion;
using Microsoft.IdentityModel.Tokens;

namespace Security.securityToken
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(User user)
        {
            // Claim list is
            var  claims = new List<Claim>{
                    new Claim(JwtRegisteredClaimNames.NameId , user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription =  new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = credentials
            };

            var tokenManager = new JwtSecurityTokenHandler();

            var token = tokenManager.CreateToken(tokenDescription);

            return tokenManager.WriteToken(token);
        }
    }
}