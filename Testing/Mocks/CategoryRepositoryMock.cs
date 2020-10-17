using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Testing.Mocks
{
    public class CategoryRepositoryMock : ICategoryRepository
    {
        public List<Category> Categories { get; set; } = new List<Category>();

        public CategoryRepositoryMock(int objectsCount)
        {
            for (int i = 1; i <= objectsCount; i++)
            {
                Categories.Add(new Category
                {
                    Id = i,
                    Name = $"CategoryName-{i}",
                    Weight = i * 10
                });
            }
        }
        public IQueryable<Category> GetAll()
        {
            return Categories.AsQueryable();
        }

        public IQueryable<Category> Get(Expression<Func<Category, bool>> predicate)
        {
            return Categories.AsQueryable().Where(predicate);
        }

        public void Add(Category entity)
        {
            var lastObject = Categories.FindLast(c => true);
            entity.Id = lastObject?.Id ?? 0;
            Categories.Add(entity);
        }

        public void Delete(Category entity)
        {
            Categories.Remove(entity);
        }

        public void Update(Category entity)
        {
            var category = Categories.FirstOrDefault(c => c.Id == entity.Id);
            category = category != null ? entity : null;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
