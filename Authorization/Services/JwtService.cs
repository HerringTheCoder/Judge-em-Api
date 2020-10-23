﻿using Authorization.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Storage.Tables;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Storage;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

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
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.AuthenticationMethod, user.ProviderName.ToString()),
                }),
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtToken:TokenExpiry"])),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["JwtToken:Audience"],
                Issuer = _configuration["JwtToken:Issuer"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
