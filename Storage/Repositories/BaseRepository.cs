using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;

namespace Storage.Repositories
{
    public abstract class BaseRepository<TContext, TEntity> : IBaseRepository<TEntity>
    where TEntity : class 
    where TContext : DbContext
    {
        protected TContext Context;

        public BaseRepository(TContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
