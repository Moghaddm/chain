using Chain.Domain.Core.Entities;

namespace Chain.Domain.Core.IReposotories;
public interface ICommentRepository
{
    ValueTask<long> AddComment(int productId,Comment comment);
    ValueTask<long> RemoveComment(int commentId);
    ValueTask<List<Comment>> GetProductComments(int productId);
}