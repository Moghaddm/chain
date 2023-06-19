using Chain.Domain.Core.Entities;

namespace Chain.Domain.IRepositories.Core;

public interface IProductRepository : IRepository<Product>
{
    ValueTask UpdateProduct(long id, Product product);
    ValueTask RemoveProduct(long id);
}
