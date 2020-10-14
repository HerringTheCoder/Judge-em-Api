using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface ISummaryService
    {
        public Summary Generate(int gameId);
    }
}
