using Authorization.Requests;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Storage;
using Storage.Tables;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization.Services
{
    class AuthenticationService : Interfaces.IAuthenticationService
    {
        private readonly IJwtService _jwtService;
        private readonly JudgeContext _judgeContext;

        public AuthenticationService(IJwtService jwtService, JudgeContext context)
        {
            _jwtService = jwtService;
            _judgeContext = context;
        }

        public Task Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(RegisterRequest request)
        {
            
            var newUser = new User()
            {
                Email = request.Email,
                Nickname = request.Nickname,
            };
            _judgeContext.Users.Add(newUser);
            await _judgeContext.SaveChangesAsync();
            return newUser;
        }
        
        public async Task<JwtSecurityToken> AuthenticationResponse(AuthenticateResult result)
        {

            var email = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var name = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var providerId = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var providerName = result.Principal.Claims.FirstOrDefault().Issuer;

            if (_judgeContext.Users.Any(i => i.Email != email ))
            {
                var newUser = new User()
                {
                    Email = email,
                    Name = name,
                    ProviderId = providerId,
                    ProviderName = providerName
                };
                await _judgeContext.Users.AddAsync(newUser);
                await _judgeContext.SaveChangesAsync();
            }
            var token =_jwtService.GenerateJwtToken(email);

            return token;
        }

        public Task FacebookLogin()
        {
            throw new NotImplementedException();
        }

        public Task GoogleLogin()
        {
            throw new NotImplementedException();
        }
    }
}
