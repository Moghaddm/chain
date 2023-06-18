namespace Chain.Domain.Core.Entities;

public class Product : Entity
{
    public string Name { get; private set; }
    public string FullEnglishName { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public Price Price { get; private set; }
    public Rate Rate { get; }
    public IReadOnlyList<Comment> Comments { get; }
    public List<Attachment> Attachments { get; private set; }
    public Company Company { get; private set; }
    public Category Category { get; private set; }
    public double Suggest
    {
        get
        {
            return (double)Comments.TakeWhile(comment => comment.Suggest).Count()
                * 100
                / Comments.Count();
        }
    }

    public Product(
        string name,
        string fullEnglishName,
        string description,
        Price price,
        int quantity,
        Company company,
        Category category
    )
    {
        ValidateMainInformations(name, fullEnglishName, description, quantity);

        Name = name;
        Description = description;
        FullEnglishName = fullEnglishName;
        Price = price;
        Quantity = quantity;
        Company = new Company(company.Name);
        Category = category;
    }

    private void ValidateMainInformations(
        string name,
        string fullEnglishName,
        string description,
        int quantity
    )
    {
        if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException($"Invalid {nameof(name)}");
        if (String.IsNullOrEmpty(description))
            throw new ArgumentNullException($"Invalid {nameof(description)}");
        if (String.IsNullOrEmpty(fullEnglishName))
            throw new ArgumentNullException($"Invalid {nameof(fullEnglishName)}");
        if (quantity <= 0)
            throw new ArgumentException($"Invalid {nameof(quantity)}");
    }

    public void UpdateProduct(
        string name,
        string fullEnglishName,
        string description,
        Price price,
        int quantity,
        Company company,
        Category category
    )
    {
        ValidateMainInformations(name, fullEnglishName, description, quantity);

        Name = name;
        Description = description;
        FullEnglishName = fullEnglishName;
        Price = price;
        Quantity = quantity;
        Company = new Company(company.Name);
        Category = category;
    }

    public void AddAttachment(Func<Attachment, bool> validateAttachment, Attachment attachment)
    {
        validateAttachment = (attachment) =>
        {
            if (attachment is null)
                throw new ArgumentException($"Invalid {nameof(attachment)}");
            else
                return true;
        };
        Attachments.Add(attachment);
    }

    public void IncreaseQuantity() => Quantity++;

    public void DecreaseQuantity() => Quantity--;
}
