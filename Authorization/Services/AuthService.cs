using System.IO;
using Authorization.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Authorization.Models;
using Core.Helpers;
using Microsoft.Extensions.Configuration;

namespace Authorization.Services
{
    class AuthService : IAuthService
    {
        private const int GuestNicknameLength = 6;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IFacebookApiService _facebookApiService;
        private readonly IConfiguration _configuration;

        public AuthService(IJwtService jwtService, IUserRepository userRepository, IFacebookApiService facebookApiService, IConfiguration configuration)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _facebookApiService = facebookApiService;
            _configuration = configuration;
        }

        public string GetUserToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.AuthenticationMethod, user.ProviderName),
                new Claim(ClaimTypes.Role, "User")
            };

            string token = _jwtService.GenerateJwtToken(claims);
            return token;
        }

        public string GetGuestToken()
        {
            string nickname = "Guest" + CodeGenerator.GenerateNumerical(GuestNicknameLength);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, nickname),
                new Claim(ClaimTypes.AuthenticationMethod, "Guest"),
                new Claim(ClaimTypes.Role, "Guest"),
            };
            string token = _jwtService.GenerateJwtToken(claims);
            return token;
        }

        public async Task<User> AuthorizeFacebookUser(string accessToken)
        {
            var fbConfig = _configuration.GetSection("Authentication:Facebook");
            var request = _facebookApiService.PrepareProfileFetchRequest(HttpMethod.Get, fbConfig["GetUserPath"], accessToken);
            var response = await _facebookApiService.SendRequestAsync(request, fbConfig["GraphHostname"]);
            var user = new User();
            if (response.Content != null)
            {
                Stream json = await response.Content.ReadAsStreamAsync();
                FacebookProfile fbProfile = await JsonSerializer.DeserializeAsync<FacebookProfile>(json,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                user = await _userRepository.GetFirstByFilterAsync(u => u.Email == fbProfile.Email);
                if (fbProfile.Email != null && fbProfile.Name != null && fbProfile.Id != null && user == null)
                {
                    user = new User
                    {
                        Email = fbProfile.Email,
                        Name = fbProfile.Name,
                        ProviderId = fbProfile.Id,
                        ProviderName = "Facebook"
                    };
                    await _userRepository.AddAsync(user);
                }
            }

            return user;
        }
    }
}
