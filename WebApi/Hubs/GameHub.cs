using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    [Authorize]
    public class GameHub : Hub<IGameClient>
    {
        private readonly IGameService _gameService;
        private readonly IItemService _itemService;
        private readonly ISummaryService _summaryService;

        public GameHub(IGameService gameService, IItemService itemService, ISummaryService summaryService)
        {
            _gameService = gameService;
            _itemService = itemService;
            _summaryService = summaryService;
        }

        public async Task ConnectToGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
                await Clients.Group(gameCode).SendMessage($"Player {Context.User.Identity.Name} has joined!");
            }
            else
            {
                await Clients.Caller.SendMessage($"Game not found");
            }
        }

        public async Task DisconnectFromGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
                await Clients.Group(gameCode).SendMessage($"Player {Context.User.Identity.Name} has left.");
            }
            else
            {
                await Clients.Caller.SendMessage("Game not found.");
            }
        }

        public async Task AddItem(ItemCreateRequest request, string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _itemService.Add(request);
                await Clients.Group(gameCode).RefreshItemList(await _itemService.GetItemsByGameId(gameId));
            }
            else
            {
                await Clients.Caller.SendMessage("Could not add item. Current game not found.");
            }
        }

        public async Task RemoveItem(int itemId, string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _itemService.Remove(itemId);
                var items = await _itemService.GetItemsByGameId(gameId);
                await Clients.Group(gameCode).RefreshItemList(items);
            }
            else
            {
                await Clients.Caller.SendMessage("Failed to delete item. Current game not found.");
            }
        }

        [Authorize(Policy = "RequireMasterRole")]
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

        [Authorize(Policy = "RequireMasterRole")]
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

        [Authorize(Policy = "RequireMasterRole")]
        public async Task DisbandGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _gameService.DisbandGame(gameId);
                await Clients.Group(gameCode).DisbandGame("Game has been canceled.");
            }
            else
            {
                await Clients.Caller.SendMessage("Failed to disband game. Game not found.");
            }
        }
    }
}
