namespace Chain.Domain.Entities;

public sealed class Company : Entity
{
    public string Name { get; private set; }

    private Company()
    {
        
    }

    public Company(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Invaild Name Company.");

        Name = name;
    }

    public void UpdateName(string name) => Name = name;

    public IReadOnlyList<Product> Products { get; private set; }
}