using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class GameRepositoryMock : IGameRepository
    {
        public List<Game> Games { get; set; } = new List<Game>();

        public GameRepositoryMock(int objectsCount)
        {
            for (int i = 1; i <= objectsCount; i++)
            {
                Games.Add(new Game
                {
                    Id = i,
                    Name = $"Name-{i}",
                    Code = $"Code-{i}",
                });
            }
        }

        public void Add(Game entity)
        {
            var lastObject = Games.FindLast(c => true);
            entity.Id = lastObject?.Id ?? 0;
            Games.Add(entity);
        }

        public void Delete(Game entity)
        {
            Games.Remove(entity);
        }

        public IQueryable<Game> Get(Expression<Func<Game, bool>> predicate)
        {
            return Games.AsQueryable().Where(predicate);
        }

        public IQueryable<Game> GetAll()
        {
            return Games.AsQueryable();
        }

        public void Update(Game entity)
        {
            var game = Games.FirstOrDefault(g => g.Id == entity.Id);
            game = game != null ? entity : null;
        }
        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public IQueryable<Game> GetGameWithSingleItemRatings(int gameId, int itemId)
        {
            return Get(g => g.Id == gameId)
                .Include(g => g.Items.FirstOrDefault(i => i.Id == itemId))
                .ThenInclude(i => i.Ratings);
        }

        public int GetActiveGameIdByCode(string gameCode)
        {
            return GetAll()
                .Where(g => g.Code == gameCode && g.FinishedAt == DateTime.MinValue)
                .Select(g => g.Id)
                .FirstOrDefault();
        }
    }
}
