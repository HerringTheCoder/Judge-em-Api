using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IGameService
    {
        public Game CreateGame(GameCreateRequest gameRequest, int userId);
        public void DisbandGame(int gameId);
        public void StartGame(int gameId);
        public void FinishGame(int gameId);
        public int FindActiveGameIdByCode(string gameCode);
    }
}
