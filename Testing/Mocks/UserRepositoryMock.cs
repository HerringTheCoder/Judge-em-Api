using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class UserRepositoryMock : IUserRepository
    {
        public List<User> Users { get; set; } = new List<User>();

        public UserRepositoryMock(int objectCount)
        {
            for (int i = 1; i <= objectCount; i++)
            {
                Users.Add(new User
                {
                    Id = i,
                    Email = $"Email-{i}",
                    Name = $"Name-{i}",
                    ProviderId = $"ProviderId-{i}",
                    ProviderName = $"ProviderName-{i}"
                });
            }
                
        }

        public void Add(User entity)
        {
            var lastObject = Users.FindLast(c => true);
            entity.Id = lastObject?.Id ?? 0;
            Users.Add(entity);
        }

        public void Delete(User entity)
        {
            Users.Remove(entity);
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
        {
            return Users.AsQueryable().Where(predicate);
        }

        public IQueryable<User> GetAll()
        {
            return Users.AsQueryable();
        }

        public void Update(User entity)
        {
            var user = Users.FirstOrDefault(u => u.Id == entity.Id);
            user = user != null ? entity : null;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
