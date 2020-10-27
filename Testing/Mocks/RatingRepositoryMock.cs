using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class RatingRepositoryMock : IRatingRepository
    {
        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public RatingRepositoryMock(int objectsCount)
        {
            for (int i = 1; i <= objectsCount; i++)
            {
                Ratings.Add(new Rating
                {
                    Id = i,
                    TotalScore = i,
                    ItemId = i,
                    PlayerProfileId = $"PlayerProfileId-{i}",
                });
            }
        }

        public void Add(Rating entity)
        {
            var lastObject = Ratings.FindLast(c => true);
            entity.Id = lastObject?.Id ?? 0;
            Ratings.Add(entity);
        }

        public void Delete(Rating entity)
        {
            Ratings.Remove(entity);
        }

        public IQueryable<Rating> Get(Expression<Func<Rating, bool>> predicate)
        {
            return Ratings.AsQueryable().Where(predicate);
        }

        public IQueryable<Rating> GetAll()
        {
            return Ratings.AsQueryable();
        }

        public void Update(Rating entity)
        {
            var rating = Ratings.FirstOrDefault(r => r.Id == entity.Id);
            rating = rating != null ? entity : null;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
