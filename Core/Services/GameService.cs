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
            var activeCodes = _gameRepository
                .Get(g => g.Code != null && g.FinishedAt != null)
                .Select(g => g.Code)
                .ToList();
            string code = "";
            while (activeCodes.Contains(code))
            {
                code = CodeGenerator.Generate(CodeLength);
            }
            var game = new Game
            {
                MasterId = userId,
                Name = request.Name,
                Code = code
            };
            _gameRepository.Add(game);
            await _gameRepository.SaveChangesAsync();
            return game;
        }

        public async Task DisbandGame(int id)
        {
            var game = _gameRepository.Get(g => g.Id == id).First();
            _gameRepository.Delete(game);
            await _gameRepository.SaveChangesAsync();
        }

        public async Task StartGame(int id)
        {
            var game = _gameRepository.Get(g => g.Id == id).First();
            if (game != null)
                game.StartedAt = DateTime.Now;
            _gameRepository.Update(game);
            await _gameRepository.SaveChangesAsync();
        }

        public async Task FinishGame(int id)
        {
            var game = _gameRepository.Get(g => g.Id == id).First();
            if (game != null)
            {
                game.FinishedAt = DateTime.Now;
                game.Code = null;
            }
            _gameRepository.Update(game);
            await _gameRepository.SaveChangesAsync();
        }

        public int FindActiveGameIdByCode(string gameCode)
        {
            return _gameRepository.GetActiveGameIdByCode(gameCode);
        }

        public async Task<(int ratingsCount, int expectedRatingsCount)> GetVotingStatus(int gameId, int itemId)
        {
            int ratingsCount = 0;
            int expectedRatingsCount = 0;
            var game = await _gameRepository.GetGameWithSingleItemRatings(gameId, itemId).FirstOrDefaultAsync();
            if (game != null)
            {
                var ratings = game.Items.First().Ratings;
                expectedRatingsCount = ConnectionObserver.ConnectionStates.Count(entry => entry.Value == game.Code);
            }

            return (ratingsCount, expectedRatingsCount);
        }
    }
}
