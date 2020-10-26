using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class PlayerProfileRepository : BaseRepository<JudgeContext, PlayerProfile>, IPlayerProfileRepository
    {
        public PlayerProfileRepository(JudgeContext context) : base(context) { }
    }
}
