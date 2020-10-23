using Authorization.Requests;
using Microsoft.AspNetCore.Authentication;
using Storage.Tables;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Authorization.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<JwtSecurityToken> Register(RegisterRequest request);
        Task Logout();
        Task<JwtSecurityToken> AuthenticationResponse(AuthenticateResult result);
    }
}
