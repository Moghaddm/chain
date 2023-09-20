namespace Chain.Domain.Entities.Core;

public sealed class Category : Entity
{
    public string Title { get; private set; }
    public int LimitOrder { get; private set; }

    public Category(string title, int limitOrder)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("Invalid Title Text");
        if (limitOrder <= 1)
            throw new ArgumentOutOfRangeException("Cannot Set Limit Less Then 1.");
        Title = title;
        LimitOrder = limitOrder;
    }
    public List<Product> Products { get; }
    public List<Company> Companies { get; }
}
