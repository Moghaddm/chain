namespace Chain.Domain.Entities;

public sealed class Attachment
{
    public byte[]? Image { get; private set; }
    public string? Alt { get; private set; }
    public string? ImageTitle { get; private set; }
    public string PropertyName { get; private set; }
    public List<string> PropertyValue { get; private set; }

    public Attachment(
        string propertyName,
        List<string> propertyValue,
        byte[] image,
        string alt,
        string imageTitle
    )
    {
        ValidateInformations(propertyName, propertyValue, image, alt, imageTitle);

        Image = image;
        Alt = alt;
        ImageTitle = imageTitle;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }

    public Attachment(string propertyName, List<string> propertyValue)
    {
        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentNullException($"Invalid {nameof(propertyName)}");
        ValidateValues(propertyValue);
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }

    public void ValidateInformations(
        string propertyName,
        List<string> propertyValue,
        byte[] image,
        string alt,
        string imageTitle
    )
    {
        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentNullException($"Invalid {nameof(propertyName)}.");
        if (image is null)
            throw new ArgumentNullException("Invalid Image Byte Array.");
        if (string.IsNullOrEmpty(alt))
            throw new ArgumentNullException($"Invalid {nameof(alt)}.");
        if (string.IsNullOrEmpty(imageTitle))
            throw new ArgumentNullException($"Invalid {nameof(imageTitle)}.");
        ValidateValues(propertyValue);
    }

    private void ValidateValues(List<string> values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Invalid Value");
        }
    }

    public Product Product { get; }
}
