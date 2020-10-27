using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class PlayerProfileRepositoryMock : IPlayerProfileRepository
    {
        public List<PlayerProfile> PlayerProfiles { get; set; } = new List<PlayerProfile>();

        public PlayerProfileRepositoryMock(int objectsCount)
        {
            for (int i = 1; i <= objectsCount; i++)
            {
                PlayerProfiles.Add(new PlayerProfile
                {
                    GameId = i,
                    UserId = i,
                    Nickname = $"PlayerProfileNickname-{i}",
                });
            }
        }
        public void Add(PlayerProfile entity)
        {
            var lastObject = PlayerProfiles.FindLast(c => true);
            entity.UserId = lastObject?.UserId ?? 0;
            PlayerProfiles.Add(entity);
        }

        public void Delete(PlayerProfile entity)
        {
            PlayerProfiles.Remove(entity);
        }

        public IQueryable<PlayerProfile> Get(Expression<Func<PlayerProfile, bool>> predicate)
        {
            return PlayerProfiles.AsQueryable().Where(predicate);
        }

        public IQueryable<PlayerProfile> GetAll()
        {
            return PlayerProfiles.AsQueryable();
        }

        public void Update(PlayerProfile entity)
        {
            var playerProfile = PlayerProfiles.FirstOrDefault(p => p.UserId == entity.UserId);
            playerProfile = playerProfile != null ? entity : null;
        }
        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
