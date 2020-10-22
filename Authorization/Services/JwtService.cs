using Authorization.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Storage.Tables;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Storage;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Authorization.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly JudgeContext _judgeContext;

        public JwtService(IConfiguration configuration, JudgeContext judgeContext)
        {
            _configuration = configuration;
            _judgeContext = judgeContext;
        }
        public JwtSecurityToken GenerateJwtToken(string email)
        {
            if (_judgeContext.Users.FirstOrDefault(u => u.Email == email) == null) return null;

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
