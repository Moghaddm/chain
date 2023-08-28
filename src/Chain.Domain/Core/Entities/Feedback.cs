namespace Chain.Domain.Core.Entities;
public sealed class Feedback : Entity
{
    public string Message { get; set; }
    public Rate Rate { get; set; }
}