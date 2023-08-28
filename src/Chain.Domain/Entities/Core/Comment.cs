using Chain.Domain.Enums;
using System.Xml.Linq;

namespace Chain.Domain.Entities.Core;

public sealed class Comment : Entity
{
    public string Gmail { get; private set; }
    public string WriterAlias { get; private set; }
    public string Description { get; private set; }
    public bool Suggest { get; private set; }
    public int VoteUps { get; set; }
    public int VoteDowns { get; set; }
    public DateTimeOffset DateTimeCommented { get; private set; }

    public void AddComment(
        string gmail,
        string writerAlias,
        string description,
        bool suggest,
        DateTimeOffset dateTime
    )
    {
        if (string.IsNullOrEmpty(gmail))
            throw new ArgumentNullException($"Invalid {nameof(gmail)}");
        if (string.IsNullOrEmpty(writerAlias))
            throw new ArgumentNullException($"Invalid {nameof(writerAlias)}");
        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException($"Invalid {nameof(description)}");

        Gmail = gmail;
        WriterAlias = writerAlias;
        Description = description;
        Suggest = suggest;
        DateTimeCommented = dateTime;
    }

    public void AddVoteProduct(Votes vote)
    {
        _ = vote switch
        {
            Votes.VoteUp => VoteUps++,
            Votes.VoteDown => VoteDowns++
        };
    }

    public Product Product { get; set; }
}
