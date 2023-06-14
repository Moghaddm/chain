namespace Chain.Domain.Core.Entities;

public class Product : Entity
{
    public string Name { get; init; }
    public string FullEnglishName { get; init; }
    public string Description { get; init; }
    public int Quantity { get; private set; }
    public long Price { get; private set; }
    public Rate Rate { get; private set; }
    public List<Comment> Comments { get; set; }
    public List<Attachment> Attachments { get; set; }
    public Company Company { get; set; }

    public Product(
        string name,
        string fullEnglishName,
        string description,
        int price,
        Company company
    )
    {
        if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException($"Invalid {nameof(name)}");
        if (String.IsNullOrEmpty(description))
            throw new ArgumentNullException($"Invalid {nameof(description)}");
        if (String.IsNullOrEmpty(fullEnglishName))
            throw new ArgumentNullException($"Invalid {nameof(fullEnglishName)}");
        if (price <= 0)
            throw new ArgumentException($"Invalid {nameof(price)}");
        Company = new Company(company.Name);
    }

    public void IncreaseQuantity() => Quantity++;

    public void DecreaseQuantity() => Quantity--;
}
