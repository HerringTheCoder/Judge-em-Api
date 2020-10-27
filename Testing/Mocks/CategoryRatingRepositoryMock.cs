using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class CategoryRatingRepositoryMock : ICategoryRatingRepository
    {
        public List<CategoryRating> CategoryRatings { get; set; } = new List<CategoryRating>();

        public CategoryRatingRepositoryMock(int objectsCount)
        {
            for (int i = 1; i <= objectsCount; i++)
            {
                CategoryRatings.Add(new CategoryRating
                {
                    CategoryId = i,
                    RatingId = i,
                    Score = i,
                });
            }
        }
        public void Add(CategoryRating entity)
        {
            var lastObject = CategoryRatings.FindLast(c => true);
            entity.CategoryId = lastObject?.CategoryId ?? 0;
            CategoryRatings.Add(entity);
        }

        public void Delete(CategoryRating entity)
        {
            CategoryRatings.Remove(entity);
        }

        public IQueryable<CategoryRating> Get(Expression<Func<CategoryRating, bool>> predicate)
        {
            return CategoryRatings.AsQueryable().Where(predicate);
        }

        public IQueryable<CategoryRating> GetAll()
        {
            return CategoryRatings.AsQueryable();
        }

        public void Update(CategoryRating entity)
        {
            var categoryRating = CategoryRatings.FirstOrDefault(c => c.CategoryId == entity.CategoryId);
            categoryRating = categoryRating != null ? entity : null;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
