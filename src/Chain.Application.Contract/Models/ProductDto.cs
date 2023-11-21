using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Domain.Entities;

namespace Chain.Application.Models
{
    public record OneProductDto(
        Guid id,
        string Name,
        string FullEnglishName,
        string Description,
        int Quantity,
        long Price,
        RateModel Rate,
        CompanyDto Company,
        CategoryDto Category,
        IEnumerable<CommentDto> Comments,
        List<AttachmentDto> Attachments);

    public record ProductDto(
        Guid id,
        string Name,
        string FullEnglishName,
        string Description,
        int Quantity,
        long Price,
        CompanyDto Company,
        CategoryDto Category,
        List<AttachmentDto> Attachments);

    public record CreateEditProductDto(
        string Name,
        string FullEnglishName,
        string Description,
        int Quantity,
        long Price,
        Guid CompanyId,
        Guid CategoryId,
        List<AttachmentDto> Attachments);
}
