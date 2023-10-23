using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;

public interface IOrderRepository
{
    Task AddToOrder(long orderId);
    Task MinusToOrder(long orderId);
}
