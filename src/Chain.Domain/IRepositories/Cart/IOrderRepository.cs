using Chain.Domain.Entities.Cart;

namespace Chain.Domain.IRepositories.Cart;

public interface IOrderRepository : IRepository<Order>
{
    Task AddToOrder(long orderId);
    Task MinusToOrder(long orderId);
}
