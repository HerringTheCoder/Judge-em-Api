using System.Threading.Tasks;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> CreateGame(GameCreateRequest gameRequest, int userId);
        Task DisbandGame(int gameId);
        Task StartGame(int gameId);
        Task FinishGame(int gameId);
        int FindActiveGameIdByCode(string gameCode);
        Task<(int ratingsCount, int expectedRatingsCount)> GetVotingStatus(int gameId, int itemId);
    }
}
