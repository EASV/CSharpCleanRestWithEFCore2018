using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomerApp.Core.Entity;
using Microsoft.IdentityModel.Tokens;

namespace CustomerApp.Infrastructure.Data.Managers
{
    public class TokenManager
    {
        private readonly string _jwtKey;
        private readonly double _jwtExpireDays;
        private readonly string _jwtIssuer;

        public TokenManager(string jwtKey, double jwtExpireDays, string jwtIssuer)
        {
            _jwtKey = jwtKey;
            _jwtExpireDays = jwtExpireDays;
            _jwtIssuer = jwtIssuer;
        }
        
        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userName", user.UserName),
                new Claim("role", user.Role.Name),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtExpireDays);

            var token = new JwtSecurityToken(
                _jwtIssuer,
                _jwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}