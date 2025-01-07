using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using business_layer.Contracts;
using domain_layer;
using Microsoft.IdentityModel.Tokens;

namespace security_layer.TokensManager
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(SystemUser user, List<string> roles)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            if (roles != null)
            {
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DigitalsoftToken"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenManager = new JwtSecurityTokenHandler();
            var token = tokenManager.CreateToken(tokenDescripcion);

            return tokenManager.WriteToken(token);

        }
    }
}
