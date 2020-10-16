using System;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    [Authorize]
    public partial class GameHub : Hub<IGameClient>
    {
        private readonly IGameService _gameService;
        private readonly IItemService _itemService;
        private readonly ISummaryService _summaryService;
        private readonly IRatingService _ratingService;

        public GameHub(IGameService gameService, IItemService itemService, ISummaryService summaryService, IRatingService ratingService)
        {
            _gameService = gameService;
            _itemService = itemService;
            _summaryService = summaryService;
            _ratingService = ratingService;
        }

        public async Task ConnectToGame(string gameCode)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
                await Clients.Group(gameCode).SendMessage($"Player {Context.User.Identity.Name} has joined!");
                ConnectionObserver.ConnectionStates.Add(Context.ConnectionId, gameCode);
            }
            else
            {
                await Clients.Caller.SendMessage($"Game not found");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionObserver.ConnectionStates.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
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
                await _itemService.Add(request, gameId);
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

        public async Task AddRating(string gameCode, RatingCreateRequest request)
        {
            var gameId = _gameService.FindActiveGameIdByCode(gameCode);
            if (gameId != 0)
            {
                await _ratingService.AddRating(request, Context.UserIdentifier);
                var (ratingsCount, expectedRatingsCount) = await _gameService.GetVotingStatus(gameId, request.ItemId);
                await Clients.Group(gameCode).RefreshVotingProgress(ratingsCount, expectedRatingsCount);
            }
            else
            {
                await Clients.Caller.SendMessage("Failed to add rating. Current game not found.");
            }
        }
    }
}
