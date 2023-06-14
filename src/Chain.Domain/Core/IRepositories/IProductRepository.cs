using Chain.Domain.Core.Entities;

namespace Chain.Domain.IRepositories.Core;

public interface IProductRepository
{
    ValueTask<List<Product>> GetProducts();
    ValueTask<Product> GetProduct(int id);
}
