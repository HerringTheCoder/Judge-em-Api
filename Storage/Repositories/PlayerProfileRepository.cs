using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class PlayerProfileRepository : BaseRepository<JudgeContext, PlayerProfile>, IPlayerProfileRepository
    {
        public PlayerProfileRepository(JudgeContext context) : base(context) { }

        public async Task<string> GetProfileIdMatchingUserIdAndGameId(int userId, int gameId)
        {
            return await Context.PlayerProfiles
                .Where(pp => pp.GameId == gameId && pp.UserId == userId)
                .Select(pp => pp.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PlayerProfile>> GetPlayerProfilesByUserIdWithFinishedGames(int userId)
        {
            return await Context.PlayerProfiles
                .Where(p => p.UserId == userId)
                .Where(p => p.Game.FinishedAt != DateTime.MinValue)
                .Include(p => p.Game)
                .ThenInclude(g => g.Summary)
                .ToListAsync();
        }
    }
}
