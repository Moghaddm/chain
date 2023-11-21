using Chain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;

namespace Chain.Application.Contract.Ports.Services
{
    public interface ICommentService
    {
        Task AddOnProduct(Guid productId, CommentDto comment);
        Task DeleteFromProduct(Guid id,Guid productId);
        Task UpdateOnProduct(Guid id, CommentDto comment);
        ValueTask<CommentDto> Get(Guid id);
        ValueTask<IEnumerable<CommentDto>> GetProductComments(Guid productId);
    }
}
