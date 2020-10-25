using Authorization.Requests;
using Authorization.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Internal;
using Storage.Tables;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization.Services
{
    class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthService(IJwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<string> Register(RegisterRequest request)
        {
            var newUser = new User()
            {
                Email = request.Email
            };
            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();

            var token = _jwtService.GenerateJwtToken(newUser);
            return token;
        }

        public async Task<string> GetToken(AuthenticateResult result)
        {
            var email = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _userRepository.Get(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                var name = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                var providerId = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var providerName = result.Principal.Claims.FirstOrDefault().Issuer;

                user = new User()
                {
                    Email = email,
                    Name = name,
                    ProviderId = providerId,
                    ProviderName = providerName
                };
                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
            }
            var token = _jwtService.GenerateJwtToken(user);

            return token;
        }
    }
}
