using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Dto;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var game = _gameRepository.Get(g => g.Id == gameId)
                .Include(g => g.Master)
                .Include(g => g.Items)
                .ThenInclude(i => i.Ratings)
                .ThenInclude(r => r.PlayerProfile)
                .FirstOrDefault();
            var gameSummaryDto = new GameSummaryDto(game);
            string jsonResult = JsonSerializer.Serialize(gameSummaryDto);
            var summary = new Summary
            {
                GameId = gameId,
                Result = jsonResult
            };

            var existingSummary = _summaryRepository.Get(s => s.GameId == gameId).FirstOrDefault();
            if (existingSummary != null)
                _summaryRepository.Update(summary);
            else
                _summaryRepository.Add(summary);

            await _summaryRepository.SaveChangesAsync();
            return summary;
        }

        public Summary GetByGameId(int gameId)
        {
            var summary = _summaryRepository.Get(s => s.GameId == gameId).FirstOrDefault();
            return summary;
        }

        public async Task DeleteAsync(int id)
        {
            var summary = _summaryRepository.Get(s => s.Id == id).FirstOrDefault();
            _summaryRepository.Delete(summary);
            await _summaryRepository.SaveChangesAsync();
        }

        public async Task<List<UserSummaryDto>> GetSummariesByUserId(int userId)
        {
            var playerProfiles = await _playerProfileRepository.GetAll()
                .Where(p => p.UserId == userId)
                .Where(p => p.Game.FinishedAt != DateTime.MinValue)
                .Include(p => p.Game)
                    .ThenInclude(g => g.Summary)
                .ToListAsync();

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
