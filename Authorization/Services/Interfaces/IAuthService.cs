using System.Threading.Tasks;
using Storage.Tables;

namespace Authorization.Services.Interfaces
{
    public interface IAuthService
    {
        string GetUserToken(User user);
        string GetGuestToken();
        Task<User> AuthorizeFacebookUser(string accessToken);
    }
}
