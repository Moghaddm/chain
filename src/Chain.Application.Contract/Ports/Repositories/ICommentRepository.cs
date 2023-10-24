using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;
public interface ICommentRepository : IBasicRepository<Comment,Guid> {}
