using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;

public interface IProductRepository
{
    ValueTask RemoveProduct(Product product);
    ValueTask<Guid> CreateAsync(Product product);
    ValueTask<Product?> GetByIdAsync(Guid id);
    ValueTask<List<Product?>> GetAsync();
}
