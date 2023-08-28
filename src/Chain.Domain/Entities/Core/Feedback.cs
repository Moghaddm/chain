namespace Chain.Domain.Entities.Core;
public sealed class Feedback : Entity
{
    public string Message { get; set; }
    public Rate Rate { get; set; }
}