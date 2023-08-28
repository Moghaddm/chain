using Chain.Domain.Core.Entities;

namespace Chain.Domain.Core.IRepositories;
public interface ICompanyRepository : IRepository<Company>
{
    ValueTask UpdateCompany(long id,Company company);
    ValueTask RemoveCompany(long id);
}