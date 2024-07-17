using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Interfaces.Base;
using OnlineStore.Models.Base;
using System.Linq.Expressions;

namespace OnlineStore.DataAccess.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is not null)
                _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
