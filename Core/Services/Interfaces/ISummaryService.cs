using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dto;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<Summary> GenerateAsync(int gameId);
        Summary GetByGameId(int gameId);
        Task DeleteAsync(int id);
        Task<List<UserSummaryDto>> GetSummariesByUserId(int userId);
    }
}
