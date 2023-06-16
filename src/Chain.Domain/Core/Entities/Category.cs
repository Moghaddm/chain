namespace Chain.Domain.Core.Entities;

public class Category : Entity
{
    public string Title { get; set; }

    public Category(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("Invalid Title Text");
        Title = title;
    }

    public List<Product> Products { get; set; }
    public List<Company> Companies { get; set; }
}
