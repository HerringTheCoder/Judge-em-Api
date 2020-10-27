using Storage.Repositories.Interfaces;
using Storage.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Testing.Mocks
{
    class SummaryRepositoryMock : ISummaryRepository
    {
        public List<Summary> Summaries { get; set; } = new List<Summary>();

        public SummaryRepositoryMock(int objectsCount)
        {
            for (int i = 1; i <= objectsCount; i++)
            {
                Summaries.Add(new Summary
                {
                    Id = i,
                    GameId = i,
                    Result = $"Result-{i}"
                });
            }
        }

        public void Add(Summary entity)
        {
            var lastObject = Summaries.FindLast(c => true);
            entity.Id = lastObject?.Id ?? 0;
            Summaries.Add(entity);
        }

        public void Delete(Summary entity)
        {
            Summaries.Remove(entity);
        }

        public IQueryable<Summary> Get(Expression<Func<Summary, bool>> predicate)
        {
            return Summaries.AsQueryable().Where(predicate);
        }

        public IQueryable<Summary> GetAll()
        {
            return Summaries.AsQueryable();
        }

        public void Update(Summary entity)
        {
            var summary = Summaries.FirstOrDefault(s => s.Id == entity.Id);
            summary = summary != null ? entity : null;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
