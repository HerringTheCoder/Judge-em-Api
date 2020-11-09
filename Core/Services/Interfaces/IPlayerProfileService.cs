using System.Threading.Tasks;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IPlayerProfileService
    {
        Task<PlayerProfile> CreateOrUpdatePlayerProfile(PlayerProfileCreateRequest request);
        Task<PlayerProfile> GetPlayerProfile(string id);
        Task<string> GetProfileIdByUserGame(int userId, int gameId);
    }
}
