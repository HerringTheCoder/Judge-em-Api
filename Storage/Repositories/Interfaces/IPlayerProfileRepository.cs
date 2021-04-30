using System.Collections.Generic;
using System.Threading.Tasks;
using Storage.Tables;

namespace Storage.Repositories.Interfaces
{
    public interface IPlayerProfileRepository : IBaseRepository<PlayerProfile>
    {
        Task<string> GetProfileIdMatchingUserIdAndGameId(int userId, int gameId);
        Task<List<PlayerProfile>> GetPlayerProfilesByUserIdWithFinishedGames(int userId);
    }
}
