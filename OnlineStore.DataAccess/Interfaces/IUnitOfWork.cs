using OnlineStore.DataAccess.Interfaces.Base;
using OnlineStore.Models.Base;

namespace OnlineStore.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
        Task<int> SaveChangesAsync();
    }
}
