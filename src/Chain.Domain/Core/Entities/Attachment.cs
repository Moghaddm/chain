namespace Chain.Domain.Core.Entities;
public class Attachment
{
    public byte[]? Image { get; set; }
    public string? Alt { get; set; }
    public string ImageTitle { get; set; }
    public string PropertyName { get; set; }
    public List<string> PropertyValue { get; set; }
    public Product Product { get; set; }
}