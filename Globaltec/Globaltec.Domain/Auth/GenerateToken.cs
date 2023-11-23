using Globaltec.Domain.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Globaltec.Domain.Auth
{
    public class GenerateToken
    {
        public static string GerarTokenDeAutenticacao(string usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyAuth = Encoding.ASCII.GetBytes(KeyAuth.KeyAuthToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyAuth), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
