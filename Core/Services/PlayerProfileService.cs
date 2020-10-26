using System.Linq;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class PlayerProfileService : IPlayerProfileService
    {
        private readonly IPlayerProfileRepository _playerProfileRepository;

        public PlayerProfileService(IPlayerProfileRepository playerProfileRepository)
        {
            _playerProfileRepository = playerProfileRepository;
        }
        public async Task<PlayerProfile> CreatePlayerProfile(PlayerProfileCreateRequest request)
        {
            var playerProfile = new PlayerProfile
            {
                GameId = request.GameId,
                Nickname = request.Nickname,
                UserId = request.UserId > 0 ? request.UserId : default
            };
            _playerProfileRepository.Add(playerProfile);
            await _playerProfileRepository.SaveChangesAsync();

            return playerProfile;
        }

        public async Task<PlayerProfile> GetPlayerProfile(string id)
        {
            var playerProfile = await _playerProfileRepository.Get(pp => pp.Id == id).FirstOrDefaultAsync();

            return playerProfile;
        }

        public async Task<string> GetProfileIdByUserGame(int userId, int gameId)
        {
            string profileId = await _playerProfileRepository.Get(pp => pp.GameId == gameId && pp.UserId == userId)
                .Select(pp => pp.Id).FirstOrDefaultAsync();

            return profileId;
        }
    }
}
