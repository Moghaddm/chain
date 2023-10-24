using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Models;

namespace Chain.Application.Contract.Models.Api
{
    public record ProductCreateRequest(ProductDto Product, Guid CompanyId, Guid CategoryId);
}
