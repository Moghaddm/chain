namespace Chain.Domain.Entities.Core;

public sealed class Product : Entity
{
    public string Name { get; private set; }
    public string FullEnglishName { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public Rate Rate { get; }
    public IReadOnlyList<Comment> Comments { get; }
    public List<Attachment> Attachments { get; private set; }
    public Company Company { get; private set; }
    public Category Category { get; private set; }
    public double Suggest
    {
        get => (double)Comments.TakeWhile(comment => comment.Suggest).Count()
                * 100
                / Comments.Count();
    }
    public long Price { get; private set; }

    public Product(
        string name,
        string fullEnglishName,
        string description,
        int price,
        int quantity,
        Company company,
        Category category
    )
    {
        ValidateMainInformations(name, fullEnglishName, description, price, quantity, company);
        Name = name;
        FullEnglishName = fullEnglishName;
        Description = description;
        Price = price;
        Quantity = quantity;
        Company = company;
        Category = category;
    }

    private void ValidateMainInformations(
            string name,
            string fullEnglishName,
            string description,
            int price,
            int quantity,
            Company company
        )
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException($"Invalid {nameof(name)}");
        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException($"Invalid {nameof(description)}");
        if (string.IsNullOrEmpty(fullEnglishName))
            throw new ArgumentNullException($"Invalid {nameof(fullEnglishName)}");
        if (price <= 0)
            throw new ArgumentException($"Invalid {nameof(price)}");
        if (quantity <= 0)
            throw new ArgumentException($"Invalid {nameof(quantity)}");
        _ = company ?? throw new ArgumentNullException();
    }

    public void UpdateProduct(
        string name,
        string fullEnglishName,
        string description,
        int price,
        int quantity,
        Company company,
        Category category
    )
    {
        ValidateMainInformations(name, fullEnglishName, description, price, quantity, company);

        Name = name;
        Description = description;
        FullEnglishName = fullEnglishName;
        Price = price;
        Quantity = quantity;
        Company = new Company(company.Name);
        Category = category;
    }


    public void AddAttachment(Attachment attachment)
    {
        Func<Attachment, bool> validateAttachment = (attachment) =>
        {
            if (attachment is null)
                throw new ArgumentException($"Invalid {nameof(attachment)}");
            else
                return true;
        };

        validateAttachment(attachment);

        Attachments.Add(attachment);
    }

    public void IncreaseQuantity() => Quantity++;
    public void DecreaseQuantity() => Quantity--;
}
