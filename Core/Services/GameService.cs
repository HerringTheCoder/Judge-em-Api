using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private const int CodeLength = 6;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> CreateGame(GameCreateRequest request, int userId)
        {
            var activeCodes = await _gameRepository.GetActiveGamesCodes();

            string code = "";
            do
            {
                code = CodeGenerator.Generate(CodeLength);
            } while (activeCodes.Contains(code));

            var game = new Game
            {
                MasterId = userId,
                Name = request.Name,
                Code = code
            };

            return await _gameRepository.AddAsync(game);
        }

        public async Task DisbandGame(int id)
        {
            var game = await _gameRepository.GetFirstByFilterAsync(g => g.Id == id);
            await _gameRepository.DeleteAsync(game);
        }

        public async Task StartGame(int id)
        {
            var game = await _gameRepository.GetFirstByFilterAsync(g => g.Id == id);

            if (game != null)
                game.StartedAt = DateTime.Now;

            await _gameRepository.UpdateAsync(game);
        }

        public async Task FinishGame(int id)
        {
            var game = await _gameRepository.GetFirstByFilterAsync(g => g.Id == id);
            if (game != null)
            {
                game.FinishedAt = DateTime.Now;
                game.Code = null;
            }

            await _gameRepository.UpdateAsync(game);
        }

        public async Task<int> FindActiveGameIdByCode(string gameCode)
        {
            return await _gameRepository.GetActiveGameIdByCode(gameCode);
        }

        public bool IsUserGameOwner(int? userId, int gameId)
        {
            if (userId == null)
                return false;

            var game = _gameRepository.GetFirstByFilterAsync(g => g.Id == gameId && g.MasterId == userId);
            return game != null;
        }

        public async Task<int> FindOwnedActiveGameId(string gameCode, int userId)
        {
            return await _gameRepository.GetOwnedActiveGameIdByCode(gameCode, userId);
        }

        public async Task<(int ratingsCount, int expectedRatingsCount)> GetVotingStatus(int gameId, int itemId)
        {
            int ratingsCount = 0;
            int expectedRatingsCount = 0;
            var game = await _gameRepository.GetGameWithSingleItemRatings(gameId, itemId).FirstOrDefaultAsync();
            if (game != null)
            {
                var ratings = game.Items.FirstOrDefault()?.Ratings;
                ratingsCount = ratings.Count();
                expectedRatingsCount = ConnectionObserver.ConnectionStates.Count(entry => entry.Value.Group == game.Code);
            }

            return (ratingsCount, expectedRatingsCount);
        }
    }
}
