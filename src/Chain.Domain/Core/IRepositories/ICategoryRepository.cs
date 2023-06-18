using Chain.Domain.Core.Entities;

namespace Chain.Domain.Core.IRepositories;
public interface ICategoryRepository : IRepository<Category>
{
    ValueTask UpdateCategory(long id,Category category);
    ValueTask RemoveCategory(long id);
}