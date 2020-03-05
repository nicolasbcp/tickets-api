using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Tickets_API.Models;

namespace Tickets_API.Services
{
    public static class TokenService
    {
        public static string GerarToken(Usuario usuario) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                 SigningCredentials =
                 new SigningCredentials(
                     new SymmetricSecurityKey(key),
                     SecurityAlgorithms.HmacSha256Signature)
            };
            tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}