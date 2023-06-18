using Chain.Domain.Cart.Entities;

namespace Chain.Domain.Cart.IRepositories;

public interface IOrderRepository : IRepository<Order>
{
    Task AddToOrder(long orderId);
    Task MinusToOrder(long orderId);
}
