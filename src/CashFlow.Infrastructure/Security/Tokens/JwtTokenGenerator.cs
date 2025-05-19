using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace CashFlow.Infrastructure.Security.Tokens
{
    internal class JwtTokenGenerator : IAcessTokenGenerator
    {

        private readonly uint _expirationTimeInMinutes;
        private readonly string _secretKey;

        
        public JwtTokenGenerator(string secretKey, uint expirationTimeInMinutes)
        {
            _secretKey = secretKey;
            _expirationTimeInMinutes = expirationTimeInMinutes;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserIdentifyer", user.UserIdentifyer.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescription));
        }

        private SymmetricSecurityKey SecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(_secretKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
