namespace Chain.Domain.Entities;

public sealed class Category : Entity
{
    public string Title { get; private set; }
    public int LimitOrder { get; private set; }

    private Category()
    {
        
    }

    public Category(string title, int limitOrder)
    {
        ValidateProperties(title, limitOrder);

        Title = title;
        LimitOrder = limitOrder;
    }

    public void Update(string title, int limitOrder)
    {
        ValidateProperties(title, limitOrder);

        Title = title;
        LimitOrder = limitOrder;
    }

    private void ValidateProperties(string title, int limitOrder)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("Invalid Title Text");
        if (limitOrder <= 1)
            throw new ArgumentOutOfRangeException("Cannot Set Limit Less Then 1.");
    }

    public IReadOnlyList<Product> Products { get; set; }
}
