using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Oshxona.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oshxona.Data.DbContexts;

namespace Oshxona.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly OshxonaDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(OshxonaDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await dbSet.AddAsync(entity);
            return entry.Entity;
        }

        public async ValueTask<bool> DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            return true;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
        {
            IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
            => await GetAll(expression, includes).FirstOrDefaultAsync();

        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entryentity = this.dbContext.Update(entity);

            return entryentity.Entity;
        }

        public async ValueTask SaveAsync()
            => await dbContext.SaveChangesAsync();
    }
}
