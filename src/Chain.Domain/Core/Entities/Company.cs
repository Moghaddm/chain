namespace Chain.Domain.Core.Entities;

public sealed class Company : Entity
{
    public string Name { get; set; }
    public Company(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Invaild Name Company.");
        Name = name;
    }
    public List<Product> Products { get; set; }
}