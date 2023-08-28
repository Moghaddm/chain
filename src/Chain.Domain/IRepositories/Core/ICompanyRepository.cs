using Chain.Domain.Entities.Core;

namespace Chain.Domain.IRepositories.Core;
public interface ICompanyRepository : IRepository<Company>
{
    ValueTask UpdateCompany(long id, Company company);
    ValueTask RemoveCompany(long id);
}