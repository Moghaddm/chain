namespace Chain.Domain;

public interface IUnitOfWork : IDisposable
{
    ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken);
}