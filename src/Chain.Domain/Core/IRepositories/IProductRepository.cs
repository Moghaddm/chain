using Chain.Domain.Core.Entities;

namespace Chain.Domain.IRepositories.Core;

<<<<<<< HEAD
public interface IProductRepository : IRepository<Product>
{
    ValueTask UpdateProduct(long id, Product product);
    ValueTask RemoveProduct(long id);
=======
public interface IProductRepository
{
    ValueTask<List<Product>> GetProducts();
    ValueTask<Product> GetProduct(int id);
>>>>>>> main
}
