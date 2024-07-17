using OnlineStore.DataAccess.Interfaces;
using OnlineStore.DataAccess.Interfaces.Base;
using OnlineStore.DataAccess.Repositories.Base;
using OnlineStore.Models.Base;

namespace OnlineStore.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        public UnitOfWork(ProductDbContext context)
        {
            _context = context;
        }
        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                _repositories[typeof(TEntity)] = new BaseRepository<TEntity>(_context);
            }
            return (IBaseRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
