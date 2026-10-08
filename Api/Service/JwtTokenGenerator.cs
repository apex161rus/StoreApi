using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Api.Model;

namespace Api.Service
{
    public class JwtTokenGenerator
    {
        private readonly string _secretkey;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _secretkey = configuration["AuthSettings:Secretkey"];
        }
    
        public string GenerateJwtToken(AppUser appUser, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretkey);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("FirstName", appUser.FirstName),
                    new Claim(ClaimTypes.Email ,appUser.Email),
                    new Claim(ClaimTypes.Role, String.Join(",", roles))
                }),
                Expires = DateTime.UtcNow.AddDays(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                     SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}