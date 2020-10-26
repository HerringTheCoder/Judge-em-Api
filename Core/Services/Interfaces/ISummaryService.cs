using System.Threading.Tasks;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface ISummaryService
    {
        public Task<Summary> GenerateAsync(int gameId);
        public Summary GetByGameId(int gameId);
        public Task DeleteAsync(int id);
    }
}
