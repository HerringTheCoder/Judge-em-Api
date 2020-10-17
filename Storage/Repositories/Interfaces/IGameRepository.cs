using System.Linq;
using Storage.Tables;

namespace Storage.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        IQueryable<Game> GetGameWithSingleItemRatings(int gameId, int itemId);
        int GetActiveGameIdByCode(string gameCode);
    }
}
