using Chain.Domain.Entities.Core;

namespace Chain.Domain.IRepositories.Core;
public interface ICategoryRepository : IRepository<Category>
{
    ValueTask UpdateCategory(long id, Category category);
    ValueTask RemoveCategory(long id);
}