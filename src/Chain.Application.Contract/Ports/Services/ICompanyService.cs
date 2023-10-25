using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Application.Models;

namespace Chain.Application.Contract.Ports.Services
{
    public interface ICompanyService
    {
        Task<Guid> Create(CompanyDto companyDto);
        Task Update(Guid id,CompanyDto companyDto);
        Task Delete(Guid id);
        ValueTask<CompanyDto> Get(Guid id);
        ValueTask<IEnumerable<CompanyDto>> GetAll();
        ValueTask<IEnumerable<ProductDto>> GetCompanyProducts(Guid id);
    }
}
