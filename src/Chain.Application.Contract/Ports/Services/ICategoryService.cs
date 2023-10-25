using Chain.Application.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Models;

namespace Chain.Application.Contract.Ports.Services
{
    public interface ICategoryService
    {
        Task<Guid> Create(CategoryDto categoryDto);
        Task Update(Guid id, CategoryDto categoryDto);
        Task Delete(Guid id);
        ValueTask<CategoryShowProductsDto> Get(Guid id);
        ValueTask<IEnumerable<CategoryDto>> GetAll();
        ValueTask<IEnumerable<ProductDto>> GetCategoryProducts(Guid id);
    }
}
