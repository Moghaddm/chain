using Chain.Domain;
using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;
public interface ICategoryRepository
{
    ValueTask UpdateCategory(long id, Category category);
    ValueTask RemoveCategory(long id);
}