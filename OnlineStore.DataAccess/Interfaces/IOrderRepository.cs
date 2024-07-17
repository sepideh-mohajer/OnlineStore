using OnlineStore.Models.Orders;

namespace OnlineStore.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> GetMaxOrderNumber();
    }
}
