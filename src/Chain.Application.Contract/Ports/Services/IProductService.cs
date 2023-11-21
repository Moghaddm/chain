using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Models;

namespace Chain.Application.Interfaces
{
    public interface IProductService
    {
        Task<Guid> Create(CreateEditProductDto createProductDto);
        ValueTask<IEnumerable<ProductDto>> GetAll();
        ValueTask<OneProductDto> Get(Guid id);
        Task Delete(Guid id);
        Task Update(Guid id, CreateEditProductDto editProductDto);
        Task UpdateCompany(Guid id, Guid companyId);
        Task UpdateCategory(Guid id, Guid categoryId);
    }
}
