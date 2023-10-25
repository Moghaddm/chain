namespace Chain.Domain.Entities;
public sealed class Product : Entity
{
    public string Name { get; private set; }
    public string FullEnglishName { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public Company Company { get; private set; }
    public Category Category { get; private set; }
    public double SuggestPercent
    {
        private set { SuggestPercent = value; }
        get => (double)Comments
                   .TakeWhile(comment => comment.Suggest)
                   .Count() * 100 / Comments.Count();
    }
    public long Price { get; private set; }
    private Product()
    {

    }

    public List<Attachment> Attachments { get; private set; }
    public List<Comment> Comments { get; private set; }

    public Product(
        string name,
        string fullEnglishName,
        string description,
        long price,
        int quantity,
        Company company,
        Category category,
        List<Attachment> attachments
    )
    {
        ValidateMainInformation(name, fullEnglishName, description, price, quantity);

        Name = name;
        FullEnglishName = fullEnglishName;
        Description = description;
        Price = price;
        Quantity = quantity;
        Company = company;
        Category = category;
        Comments = Enumerable.Empty<Comment>().ToList();
        Attachments = attachments;
    }

    private void ValidateMainInformation(
            string name,
            string fullEnglishName,
            string description,
            long price,
            long quantity
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
        if (quantity < 0)
            throw new ArgumentException($"Invalid {nameof(quantity)}");
    }


    public void UpdateCategory(Category category)
    {
        _ = category ?? throw new ArgumentNullException($"{nameof(category)} for update cannot be null.");

        Category = category;
    }

    public void UpdateCompany(Company company)
    {
        _ = company ?? throw new ArgumentNullException($"{nameof(company)} for update cannot be null.");

        Company = company;
    }

    public void UpdateProduct(
        string name,
        string fullEnglishName,
        string description,
        long price,
        int quantity
    )
    {
        ValidateMainInformation(name, fullEnglishName, description, price, quantity);

        Name = name;
        Description = description;
        FullEnglishName = fullEnglishName;
        Price = price;
        Quantity = quantity;
    }

    public void AddAttachment(Attachment attachment)
    {
        ValidateAttachment(attachment);

        Attachments.Add(attachment);
    }

    private Func<Attachment, bool> ValidateAttachment = (attachment) =>
    {
        if (attachment is null)
            throw new ArgumentException($"Invalid {nameof(attachment)}");
        else
            return true;
    };
}
