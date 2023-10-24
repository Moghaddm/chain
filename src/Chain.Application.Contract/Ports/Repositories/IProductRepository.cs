using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;

public interface IProductRepository : IBasicRepository<Product,Guid> {}
