using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class CategoryRatingRepository : BaseRepository<JudgeContext, CategoryRating>, ICategoryRatingRepository
    {
        public CategoryRatingRepository(JudgeContext context) : base(context){}
    }
}
