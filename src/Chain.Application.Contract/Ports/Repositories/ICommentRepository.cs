using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;
public interface ICommentRepository
{
    ValueTask<long> AddComment(int productId, Comment comment);
    ValueTask<long> RemoveComment(int commentId);
    ValueTask<List<Comment>> GetProductComments(int productId);
}