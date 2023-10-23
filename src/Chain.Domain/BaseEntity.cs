namespace Chain.Domain;
public interface IEntity<TKey>
{
    TKey Id { get; }
    string ConcurrencyStamp { get; protected set; }
}

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; private set; }
    public string ConcurrencyStamp { get; set; }
}

public class Entity : Entity<Guid> { }
