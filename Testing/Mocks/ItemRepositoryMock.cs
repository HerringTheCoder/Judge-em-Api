using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class ItemRepositoryMock : IItemRepository
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public ItemRepositoryMock(int objectsCount)
        {
            for(int i = 1; i <= objectsCount; i++)
            {
                Items.Add(new Item
                {
                    Name = $"Name-{i}",
                    Description = $"Desctription-{i}",
                    ImageLink = $"ImageLink-{i}",
                });
            }
        }
        public void Add(Item entity)
        {
            var lastObject = Items.FindLast(c => true);
            entity.Id = lastObject?.Id ?? 0;
            Items.Add(entity);
        }

        public void Delete(Item entity)
        {
            Items.Remove(entity);
        }

        public IQueryable<Item> Get(Expression<Func<Item, bool>> predicate)
        {
            return Items.AsQueryable().Where(predicate);
        }

        public IQueryable<Item> GetAll()
        {
            return Items.AsQueryable();
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public void Update(Item entity)
        {
            var item = Items.FirstOrDefault(i => i.Id == entity.Id);
            item = item != null ? entity : null;
        }
    }
}
