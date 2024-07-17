using System.Linq.Expressions;

namespace OnlineStore.DataAccess.Interfaces.Base
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
