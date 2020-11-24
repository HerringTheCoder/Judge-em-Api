using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class GameRepository : BaseRepository<JudgeContext, Game>, IGameRepository
    {
        public GameRepository(JudgeContext context) : base(context) { }

        public IQueryable<Game> GetGameWithSingleItemRatings(int gameId, int itemId)
        {
            return Get(g => g.Id == gameId)
                .Include(g => g.Items.Where(i => i.Id == itemId))
                .ThenInclude(i => i.Ratings);
        }

        public int GetActiveGameIdByCode(string gameCode)
        {
            return GetAll()
                .Where(g => g.Code == gameCode && g.FinishedAt == DateTime.MinValue)
                .Select(g => g.Id)
                .FirstOrDefault();
        }

        public int GetOwnedActiveGameIdByCode(string gameCode, int userId)
        {
            return GetAll()
                .Where(g => g.Code == gameCode && g.FinishedAt == DateTime.MinValue && g.MasterId == userId)
                .Select(g => g.Id)
                .FirstOrDefault();
        }
    }
}
