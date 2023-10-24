using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Domain.Entities;

namespace Chain.Application.Models
{
    public record OneProductDto(string Name,
        string FullEnglishName,
        string Description,
        int Quantity,
        long Price,
        Company Company,
        Category Category,
        IEnumerable<CommentDto> Comments,
        List<Attachment> Attachments);

    public record ProductDto(string Name,
        string FullEnglishName,
        string Description,
        int Quantity,
        long Price,
        Company Company,
        Category Category,
        List<Attachment> Attachments);
}
