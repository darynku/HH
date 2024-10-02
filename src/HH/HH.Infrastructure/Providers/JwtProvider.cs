using HH.Domain.Entitties;
using HH.Domain.Interfaces;
using HH.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace HH.Infrastructure.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string Generate(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new ClaimsIdentity([
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = creds,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience
            };
            var token = new JsonWebTokenHandler().CreateToken(tokenDescription);
            return token;
        }
    }
}
