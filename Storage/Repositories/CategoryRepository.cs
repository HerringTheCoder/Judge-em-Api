using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class CategoryRepository : BaseRepository<JudgeContext, Category>, ICategoryRepository
    {
        public CategoryRepository(JudgeContext context) : base(context){}
    }
}
