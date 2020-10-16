using System.Linq;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class GameRepository : BaseRepository<JudgeContext,Game>, IGameRepository
    {
        public GameRepository(JudgeContext context) : base (context){}

        public IQueryable<Game> GetGameWithSingleItemRatings(int gameId, int itemId)
        {
            return Get(g => g.Id == gameId)
                .Include(g => g.Items.FirstOrDefault(i => i.Id == itemId))
                .ThenInclude(i => i.Ratings);
        }
    }
}
