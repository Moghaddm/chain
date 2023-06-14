namespace Chain.Domain.Core.Entities;
public class Comment : Entity
{
    public string Gmail { get; set; }
    public string WriterAlias { get; set; }
    public string Description { get; set; }
    public int VoteUps { get; set; }
    public int VoteDowns { get; set; }
    public bool Suggest { get; set; }
}