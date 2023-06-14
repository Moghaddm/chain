namespace Chain.Domain;

public abstract class Entity<T>
{
    public T Id { get; set; }
}

public class Entity : Entity<long> { }
