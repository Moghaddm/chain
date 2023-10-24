using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;
public interface ICompanyRepository : IBasicRepository<Company,Guid> {}
