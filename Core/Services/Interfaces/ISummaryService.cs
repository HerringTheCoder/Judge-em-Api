using System.Threading.Tasks;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface ISummaryService
    {
        public Task<Summary> Generate(int gameId);
    }
}
