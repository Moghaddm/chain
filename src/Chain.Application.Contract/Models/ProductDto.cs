using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Domain.Entities;

namespace Chain.Application.Models
{
    public record ProductDto(string Name, string FullEnglishName, string Description, int Quantity, long Price,Company Company,Category Category);
}
