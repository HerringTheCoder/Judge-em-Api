using Authorization.Requests;
using System.Threading.Tasks;

namespace Authorization.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task Register(RegisterRequest request);
        Task Login(LoginRequest request);
        Task Logout();
    }
}
