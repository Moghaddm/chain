namespace Chain.Domain;
public interface IEntity<TKey>
{
    TKey Id { get; }
    Guid ConcurrencyStamp { get; protected set; }
}

public abstract class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; private set; }
    public Guid ConcurrencyStamp { get; set; }
}

public class Entity : Entity<Guid> { }
