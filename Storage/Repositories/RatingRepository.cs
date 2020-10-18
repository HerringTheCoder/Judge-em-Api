using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class RatingRepository : BaseRepository<JudgeContext, Rating>, IRatingRepository
    {
        public RatingRepository(JudgeContext context) : base(context){}
    }
}
