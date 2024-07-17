using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Interfaces;
using OnlineStore.Models.Orders;

namespace OnlineStore.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ProductDbContext _context;
        private readonly DbSet<Order> _dbSet;
        public OrderRepository(ProductDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Order>();
        }

        public async Task<int> GetMaxOrderNumber()
        {
            var maxValue = await _dbSet
                                    .Select(p => (int?)p.OrderNo)
                                    .MaxAsync() ?? 1;
            return maxValue;
        }
    }
}
