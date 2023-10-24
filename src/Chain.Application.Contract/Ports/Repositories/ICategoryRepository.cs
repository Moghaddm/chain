using Chain.Domain;
using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;
public interface ICategoryRepository : IBasicRepository<Category, Guid> {}
