using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebApi.Attributes;
using WebApi.Constants;

namespace WebApi.Hubs
{
    [Authorize]
    public partial class GameHub : Hub<IGameClient>
    {
        private readonly IGameService _gameService;
        private readonly IItemService _itemService;
        private readonly ISummaryService _summaryService;
        private readonly IRatingService _ratingService;
        private readonly IPlayerProfileService _profileService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<GameHub> _logger;

        public GameHub(IGameService gameService,
            IItemService itemService,
            ISummaryService summaryService,
            IRatingService ratingService,
            ICategoryService categoryService,
            IPlayerProfileService profileService,
            ILogger<GameHub> logger)
        {
            _gameService = gameService;
            _itemService = itemService;
            _summaryService = summaryService;
            _ratingService = ratingService;
            _profileService = profileService;
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task ConnectToGame([GameCode] string gameCode, string nickname)
        {
            _logger.LogInformation($"Client with connection id: {Context.ConnectionId} is attempting connection using code: {gameCode} under nickname: {nickname}");
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            int? userId = Context.UserIdentifier != null ? (int?)int.Parse(Context.UserIdentifier) : null;

            var playerProfile = await _profileService.CreateOrUpdatePlayerProfile(new PlayerProfileCreateRequest
            {
                GameId = gameId,
                Nickname = nickname,
                UserId = userId
            });

            await Clients.Caller.SendPlayerProfileId(playerProfile.Id);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
            ConnectionObserver.ConnectionStates[Context.ConnectionId] = new PlayerEntry
            {
                Group = gameCode,
                PlayerProfileId = playerProfile.Id,
                Nickname = playerProfile.Nickname,
            };
            await Clients.GroupExcept(gameCode, Context.ConnectionId).SendMessage($"Player {playerProfile.Nickname} has joined!", MessageType.Success);

            var playersList = ConnectionObserver.GetPlayersList(gameCode);
            await Clients.Group(gameCode).RefreshPlayersList(playersList);
            var items = await _itemService.GetItemsByGameId(gameId);
            await Clients.Caller.RefreshItemList(items);
            var categories = await _categoryService.GetCategoriesByGameId(gameId);
            await Clients.Caller.RefreshCategories(categories);

            if (_gameService.IsUserGameOwner(userId, gameId))
            {
                await Clients.Caller.AllowGameControl((int)userId);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"{gameCode}-master");
            }
            await Clients.Group($"{gameCode}-master").RequestCurrentItemId(gameCode);
            await Clients.Caller.SendMessage("Game joined successfully", MessageType.Success);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            string groupName = ConnectionObserver.GetCurrentGroupName(connectionId);
            ConnectionObserver.ConnectionStates.Remove(Context.ConnectionId);
            if (groupName != null)
            {
                var playersList = ConnectionObserver.GetPlayersList(groupName);
                await Clients.Group(groupName).RefreshPlayersList(playersList);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task DisconnectFromGame([GameCode] string gameCode)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);

            _logger.LogInformation($"Connection:{Context.ConnectionId} is leaving group {gameCode}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameCode);
            await Clients.GroupExcept(gameCode, Context.ConnectionId).SendMessage($"Player {Context.User.Identity.Name} has left.", MessageType.Warning);
            string connectionId = Context.ConnectionId;
            string groupName = ConnectionObserver.GetCurrentGroupName(connectionId);
            ConnectionObserver.ConnectionStates.Remove(Context.ConnectionId);
            if (groupName != null)
            {
                var playersList = ConnectionObserver.GetPlayersList(groupName);
                await Clients.Group(groupName).RefreshPlayersList(playersList);
            }
        }

        public async Task SynchronizeCurrentItem([GameCode] string gameCode, int itemId)
        {
            await Clients.OthersInGroup(gameCode).RefreshCurrentItemId(itemId);
        }

        public async Task AddItem(ItemCreateRequest request, string gameCode)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            await _itemService.Add(request, gameId);
            var items = await _itemService.GetItemsByGameId(gameId);
            await Clients.Group(gameCode).RefreshItemList(items);
        }

        public async Task RemoveItem(int itemId, [GameCode] string gameCode)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            await _itemService.DeleteAsync(itemId);
            var items = await _itemService.GetItemsByGameId(gameId);
            await Clients.Group(gameCode).RefreshItemList(items);

        }

        public async Task AddRating([GameCode] string gameCode, RatingCreateRequest request)
        {
            int.TryParse(Context.Items[KeyConstants.GameCode] as string, out int gameId);
            if (int.TryParse(Context.UserIdentifier, out int userId) && userId != 0)
            {
                request.PlayerProfileId = await _profileService.GetProfileIdByUserGame(userId, gameId);
            }

            await _ratingService.AddRating(request);
            var (ratingsCount, expectedRatingsCount) =
                await _gameService.GetVotingStatus(gameId, request.ItemId);
            await Clients.Group(gameCode).RefreshVotingProgress(ratingsCount, expectedRatingsCount);
        }

    }
}
