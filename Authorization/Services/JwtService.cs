using Authorization.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Storage.Tables;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Authorization.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtSecurityToken GenerateJwtToken(User user)
        {

            if (user == null) return null;
            
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["JwtToken:Issuer"],
              _configuration["JwtToken:Issuer"],
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return token;
        }
    }
}
