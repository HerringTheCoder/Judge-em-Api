using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class GameRepository : BaseRepository<JudgeContext,Game>, IGameRepository
    {
        public GameRepository(JudgeContext context) : base (context){}
    }
}
