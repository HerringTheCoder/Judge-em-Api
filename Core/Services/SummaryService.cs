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

        public SummaryService(ISummaryRepository summaryRepository, IGameRepository gameRepository)
        {
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
    }
}
