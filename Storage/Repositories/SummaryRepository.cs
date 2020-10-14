using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class SummaryRepository : BaseRepository<JudgeContext, Summary>, ISummaryRepository
    {
        public SummaryRepository(JudgeContext context) : base(context) { }
    }

}
