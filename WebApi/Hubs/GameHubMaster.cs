using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebApi.Attributes;
using WebApi.Constants;

namespace WebApi.Hubs
{
    [Authorize]
    public partial class GameHub : Hub<IGameClient>
    {
        public async Task StartGame([GameCode] string gameCode, int itemId)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            await _gameService.StartGame(gameId);
            await Clients.Group(gameCode).RefreshCurrentItemId(itemId);
        }

        public async Task FinishGame([GameCode] string gameCode)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            await _gameService.FinishGame(gameId);
            var summary = await _summaryService.GenerateAsync(gameId);
            await Clients.Group(gameCode).ShowSummary(summary);
        }

        public async Task DisbandGame([GameCode] string gameCode)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            await _gameService.DisbandGame(gameId);
            await Clients.Group(gameCode).DisbandGame("Game has been canceled.");
            ConnectionObserver.CleanConnectionGroup(gameCode);
        }

        public async Task PushItemId([GameCode] string gameCode, int itemId)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            await Clients.Group(gameCode).RefreshCurrentItemId(itemId);
        }
    }
}
