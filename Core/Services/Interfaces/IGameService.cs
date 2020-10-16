using System.Threading.Tasks;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IGameService
    {
        public Task<Game> CreateGame(GameCreateRequest gameRequest, int userId);
        public Task DisbandGame(int gameId);
        public Task StartGame(int gameId);
        public Task FinishGame(int gameId);
        public int FindActiveGameIdByCode(string gameCode);
    }
}
