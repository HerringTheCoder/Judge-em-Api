using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Storage.Repositories
{
    public class UserRepository : BaseRepository<JudgeContext, User>, IUserRepository
    {
        public UserRepository(JudgeContext context) : base(context) { }
    }
}
