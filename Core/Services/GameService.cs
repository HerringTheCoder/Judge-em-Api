using System;
using System.Linq;
using Core.Helpers;
using Core.Requests;
using Core.Services.Interfaces;
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

        public Game CreateGame(GameCreateRequest request, int userId)
        {
            var activeCodes = _gameRepository.GetAll()
                .Where(g => g.Code != null && g.FinishedAt != null)
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
            _gameRepository.SaveChanges();
            return game;
        }

        public void DisbandGame(int id)
        {
            var game = _gameRepository.Get(g => g.Id == id).First();
            _gameRepository.Delete(game);
            _gameRepository.SaveChanges();
        }

        public void StartGame(int id)
        {
            var game = _gameRepository.Get(g => g.Id == id).First();
            if (game != null)
                game.StartedAt = DateTime.Now;
            _gameRepository.Update(game);
            _gameRepository.SaveChanges();
        }

        public void FinishGame(int id)
        {
            var game = _gameRepository.Get(g => g.Id == id).First();
            if (game != null)
            {
                game.FinishedAt = DateTime.Now;
                game.Code = null;
            }
            _gameRepository.Update(game);
            _gameRepository.SaveChanges();
        }

        public int FindActiveGameIdByCode(string gameCode)
        {
            return _gameRepository.GetAll()
                    .Where(g => g.Code == gameCode && g.FinishedAt == null)
                    .Select(g => g.Id)
                .FirstOrDefault();
        }
    }
}
