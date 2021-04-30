using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Context.Games
                .Where(g => g.Id == gameId)
                .Include(g => g.Items.Where(i => i.Id == itemId))
                .ThenInclude(i => i.Ratings);
        }

        public async Task<int> GetActiveGameIdByCode(string gameCode)
        {
            return await Context.Games
                .Where(g => g.Code == gameCode && g.FinishedAt == DateTime.MinValue)
                .Select(g => g.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetOwnedActiveGameIdByCode(string gameCode, int userId)
        {
            return await Context.Games
                .Where(g => g.Code == gameCode && g.FinishedAt == DateTime.MinValue && g.MasterId == userId)
                .Select(g => g.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetActiveGamesCodes()
        {
            return await Context.Games
                .Where(g => g.Code != null && g.FinishedAt != DateTime.MinValue)
                .Select(g => g.Code)
                .ToListAsync();
        }

        public async Task<Game> GetFullGameDataById(int gameId)
        {
            return await Context.Games
                .Where(g => g.Id == gameId)
                .Include(g => g.Master)
                .Include(g => g.Items)
                    .ThenInclude(i => i.Ratings)
                        .ThenInclude(r => r.PlayerProfile)
                .FirstOrDefaultAsync();
        }
    }
}
