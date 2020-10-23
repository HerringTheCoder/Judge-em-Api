using Authorization.Requests;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace Authorization.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequest request);
        Task<string> GetToken(AuthenticateResult result);
    }
}
