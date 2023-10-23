using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;
public interface ICompanyRepository
{
    ValueTask UpdateCompany(long id, Company company);
    ValueTask RemoveCompany(long id);
}