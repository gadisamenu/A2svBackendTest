using Domain;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Application.Contracts.Services;

namespace Infrastructure.Services
{
    public class JwtService: IJwtService
    {
        public readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public JwtService(IConfiguration config)
        {
            _configuration = config;
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        public Tuple<string, double> GenerateToken(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim>{
                new(ClaimTypes.PrimarySid, user.Id),
                new("Email", user.Email)
            };
            claims.AddRange(
                from role in roles
                select new Claim(ClaimTypes.Role, role)
            );

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expiration = Convert.ToDouble(_configuration["Jwt:ExpireInMin"]);
            var jwtOptions = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiration),
                signingCredentials: creds
            );

            return Tuple.Create(_jwtTokenHandler.WriteToken(jwtOptions), expiration);
        }
    }
}