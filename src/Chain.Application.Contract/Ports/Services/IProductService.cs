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
        Task<Guid> Create(ProductDto productDto);
        ValueTask<List<ProductDto>> GetAll();
        ValueTask<ProductDto> GetById(Guid id);
        Task Delete(Guid id);
        Task Update(Guid id,ProductDto productDto);
    }
}
