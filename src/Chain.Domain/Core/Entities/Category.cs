namespace Chain.Domain.Core.Entities;
public class Category : Entity
{
    public string Title { get; set; }
    public List<Product> Products { get; set; }
    public List<Company> Companies { get; set; }

}