using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain.Application.Contract.Models
{
    public record AttachmentDto(IFormFile Image, string ImageMimeType, string? Alt, string? ImageTitle);
}
