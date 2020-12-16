using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    [Authorize]
    public partial class GameHub : Hub<IGameClient>
    {
        public async Task StartGame(string gameCode, int itemId)
        {
            var gameId = _gameService.FindOwnedActiveGameId(gameCode, int.Parse(Context.UserIdentifier));
            if (gameId != 0)
            {
                await _gameService.StartGame(gameId);
                await Clients.Group(gameCode).RefreshCurrentItemId(itemId);
            }
            else
            {
                await Clients.Caller.SendMessage($"Failed to start a game. Game not found.");
            }
        }

        public async Task FinishGame(string gameCode)
        {
            var gameId = _gameService.FindOwnedActiveGameId(gameCode, int.Parse(Context.UserIdentifier));
            if (gameId != 0)
            {
                await _gameService.FinishGame(gameId);
                var summary = await _summaryService.GenerateAsync(gameId);
                await Clients.Group(gameCode).ShowSummary(summary);
            }
            else
            {
                await Clients.Caller.SendMessage("Could not finish a game. Game not found.");
            }
        }

        public async Task DisbandGame(string gameCode)
        {
            var gameId = _gameService.FindOwnedActiveGameId(gameCode, int.Parse(Context.UserIdentifier));
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

        public async Task PushItemId(string gameCode, int itemId)
        {
            var gameId = _gameService.FindOwnedActiveGameId(gameCode, int.Parse(Context.UserIdentifier));
            if (gameId != 0)
            {
                await Clients.Group(gameCode).RefreshCurrentItemId(itemId);
            }
            else
            {
                await Clients.Caller.SendMessage("Failed to broadcast Item Id. Game not found.");
            }
        }
    }
}
