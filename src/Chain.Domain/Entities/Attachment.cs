using System.Text.Json.Serialization;

namespace Chain.Domain.Entities;

public sealed class Attachment
{
    private Attachment() { }

    public byte[]? Image { get; private set; } = default!;
    public string ImageMimeType { get; private set; } = default!;
    public string? Alt { get; private set; } = default!;
    public string? ImageTitle { get; private set; } = default!;

    [JsonConstructor]
    public Attachment(
        byte[] image,
        string alt,
        string imageTitle,
        string imageMimeType
    )
    {
        ValidateInformation(
            image,
            alt,
            imageTitle,
            imageMimeType);

        Image = image;
        ImageMimeType = imageMimeType;
        Alt = alt;
        ImageTitle = imageTitle;
    }

    private void ValidateInformation(
        byte[] image,
        string alt,
        string imageTitle,
        string imageMimeType
    )
    {
        if (image is null)
            throw new ArgumentNullException("Invalid Image Byte Array.");
        if (string.IsNullOrEmpty(alt))
            throw new ArgumentNullException($"Invalid {nameof(alt)}.");
        if (string.IsNullOrEmpty(imageTitle))
            throw new ArgumentNullException($"Invalid {nameof(imageTitle)}.");
        if (string.IsNullOrEmpty(imageMimeType))
            throw new ArgumentNullException($"Invalid {nameof(imageMimeType)}.");
    }

    private void ValidateValues(List<string> values)
    {
        foreach (var value in values)
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Invalid Value");
    }

    public Guid ProductId  { get; set; }
}
