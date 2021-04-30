using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Tables;

namespace Storage.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        IQueryable<Game> GetGameWithSingleItemRatings(int gameId, int itemId);
        Task<int> GetActiveGameIdByCode(string gameCode);
        Task<int> GetOwnedActiveGameIdByCode(string gameCode, int userId);
        Task<List<string>> GetActiveGamesCodes();
        Task<Game> GetFullGameDataById(int gameId);
    }
}
