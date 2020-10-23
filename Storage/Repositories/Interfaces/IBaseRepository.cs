using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Storage.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task SaveChangesAsync();
    }
}
