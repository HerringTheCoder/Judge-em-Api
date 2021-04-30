using Core.Requests;
using Core.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PlayerProfileService : IPlayerProfileService
    {
        private readonly IPlayerProfileRepository _playerProfileRepository;

        public PlayerProfileService(IPlayerProfileRepository playerProfileRepository)
        {
            _playerProfileRepository = playerProfileRepository;
        }

        public async Task<PlayerProfile> CreateOrUpdatePlayerProfile(PlayerProfileCreateRequest request)
        {
            PlayerProfile playerProfile = null;
            if (request.UserId != null)
            {
                playerProfile = await _playerProfileRepository
                    .GetFirstByFilterAsync(p => p.GameId == request.GameId &&
                                                p.UserId == request.UserId);
            }

            if (playerProfile != null)
            {
                playerProfile.Nickname = request.Nickname;
                await _playerProfileRepository.UpdateAsync(playerProfile);
            }
            else
            {
                playerProfile = new PlayerProfile
                {
                    GameId = request.GameId,
                    Nickname = request.Nickname,
                    UserId = request.UserId > 0 ? request.UserId : null
                };
                await _playerProfileRepository.AddAsync(playerProfile);
            }

            return playerProfile;
        }

        public async Task<PlayerProfile> GetPlayerProfile(string id)
        {
            return await _playerProfileRepository.GetFirstByFilterAsync(pp => pp.Id == id);
        }

        public async Task<string> GetProfileIdByUserGame(int userId, int gameId)
        {
            return await _playerProfileRepository.GetProfileIdMatchingUserIdAndGameId(userId, gameId);
        }
    }
}
