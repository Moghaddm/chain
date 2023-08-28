using Chain.Domain.Core.Entities;

namespace Chain.Domain.Core.IRepositories;
public interface ICommentRepository : IRepository<Comment>
{
    ValueTask<long> AddComment(int productId,Comment comment);
    ValueTask<long> RemoveComment(int commentId);
    ValueTask<List<Comment>> GetProductComments(int productId);
}