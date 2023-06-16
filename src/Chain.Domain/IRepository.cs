namespace Chain.Domain;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TEntity>
{
    ValueTask CreateAsync(TEntity entity);
    ValueTask<TEntity?> GetByIdAsync(TKey id);
    ValueTask<List<TEntity?>> GetAsync();
}

public interface IRepository<TEntity>
    where TEntity : class { }
