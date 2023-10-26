using Chain.Domain.Enums;
using System.Xml.Linq;

namespace Chain.Domain.Entities;

public sealed class Comment : Entity
{
    public string WriterAlias { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Gmail { get; private set; }
    public DateTimeOffset DateTimeCommented { get; private set; }
    public bool Suggest { get; private set; }
    public int VoteUps { get; private set; }
    public int VoteDowns { get; private set; }
    public RateNumber RateNumber { get; private set; }

    private Comment()
    {

    }

    public Comment(
        string writerAlias,
        string title,
        string description,
        string gmail,
        bool suggest,
        DateTimeOffset dateTime,
        RateNumber rateNumber)
        => ValidateSetProperties(writerAlias, title, description, gmail, suggest, dateTime, rateNumber);

    public void Update(
        string writerAlias,
        string title,
        string description,
        string gmail,
        bool suggest,
        DateTimeOffset dateTime,
        RateNumber rateNumber)
        => ValidateSetProperties(writerAlias, title, description, gmail, suggest, dateTime, rateNumber);

    private void ValidateSetProperties(
        string writerAlias,
        string title,
        string description,
        string gmail,
        bool suggest,
        DateTimeOffset dateTime,
        RateNumber rateNumber)
    {
        if (string.IsNullOrEmpty(gmail))
            throw new ArgumentNullException($"Invalid {nameof(gmail)}");
        if (string.IsNullOrEmpty(writerAlias))
            throw new ArgumentNullException($"Invalid {nameof(writerAlias)}");
        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException($"Invalid {nameof(description)}");

        Title = title;
        Gmail = gmail;
        WriterAlias = writerAlias;
        Description = description;
        Suggest = suggest;
        DateTimeCommented = dateTime;
        RateNumber = rateNumber;
    }

    public void AddVoteProduct(Votes vote)
    {
        _ = vote switch
        {
            Votes.VoteUp => VoteUps++,
            Votes.VoteDown => VoteDowns++
        };
    }

    public void RemoveVoteProduct(Votes vote)
    {
        _ = vote switch
        {
            Votes.VoteUp => VoteUps--,
            Votes.VoteDown => VoteDowns--
        };
    }

    public Product Product { get; set; }
}
