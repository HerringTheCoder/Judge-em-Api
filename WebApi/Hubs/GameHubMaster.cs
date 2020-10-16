using System.Threading.Tasks;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    [Authorize(Policy = "RequireMasterRole")]
    public partial class GameHub : Hub<IGameClient>
    {
        public async Task StartGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _gameService.StartGame(gameId);
                await Clients.Group(gameCode).RefreshItemIndex(1);
            }
            else
                await Clients.Caller.SendMessage($"Failed to start a game. Game not found.");
        }

        public async Task FinishGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _gameService.FinishGame(gameId);
                var summary = await _summaryService.Generate(gameId);
                await Clients.Group(gameCode).ShowSummary(summary);
            }
            else
            {
                await Clients.Caller.SendMessage("Could not finish a game. Game not found.");
            }
        }

        public async Task DisbandGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _gameService.DisbandGame(gameId);
                await Clients.Group(gameCode).DisbandGame("Game has been canceled.");
                ConnectionObserver.CleanConnectionGroup(gameCode);
            }
            else
            {
                await Clients.Caller.SendMessage("Failed to disband game. Game not found.");
            }
        }
    }
}
