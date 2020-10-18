using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class ItemRepository : BaseRepository<JudgeContext, Item>, IItemRepository
    {
        public ItemRepository(JudgeContext context) : base(context) { }
    }
}
