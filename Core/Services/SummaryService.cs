using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Dto;
using Core.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerProfileRepository _playerProfileRepository;

        public SummaryService(ISummaryRepository summaryRepository, IGameRepository gameRepository, IPlayerProfileRepository playerProfileRepository)
        {
            _playerProfileRepository = playerProfileRepository;
            _summaryRepository = summaryRepository;
            _gameRepository = gameRepository;
        }
        public async Task<Summary> GenerateAsync(int gameId)
        {
            var game = await _gameRepository.GetFullGameDataById(gameId);
            var gameSummaryDto = new GameSummaryDto(game);
            string jsonResult = JsonSerializer.Serialize(gameSummaryDto);
            var summary = new Summary
            {
                GameId = gameId,
                Result = jsonResult
            };

            var existingSummary = _summaryRepository.GetFirstByFilterAsync(s => s.GameId == gameId);
            if (existingSummary != null)
                return await _summaryRepository.UpdateAsync(summary);

            return await _summaryRepository.AddAsync(summary);
        }

        public async Task<Summary> GetByGameId(int gameId)
        {
            return await _summaryRepository.GetFirstByFilterAsync(s => s.GameId == gameId);
        }

        public async Task DeleteAsync(int id)
        {
            var summary = await _summaryRepository.GetFirstByFilterAsync(s => s.Id == id);
            await _summaryRepository.DeleteAsync(summary);
        }

        public async Task<List<UserSummaryDto>> GetSummariesByUserId(int userId)
        {
            var playerProfiles = await _playerProfileRepository.GetPlayerProfilesByUserIdWithFinishedGames(userId);

            var userSummariesDto = playerProfiles.Select(p => new UserSummaryDto
            {
                PlayerProfileId = p.Id,
                GameId = p.GameId,
                GameName = p.Game.Name,
                SummaryId = p.Game.Summary?.Id,
                FinishedAt = p.Game.FinishedAt
            }).ToList();

            return userSummariesDto;
        }
    }
}
