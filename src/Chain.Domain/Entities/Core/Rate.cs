namespace Chain.Domain.Entities.Core;

public sealed class Rate : Entity
{
    public decimal One { get; private set; }
    public decimal Two { get; private set; }
    public decimal Three { get; private set; }
    public decimal Four { get; private set; }
    public decimal Five { get; private set; }
    public List<Feedback> Feedbacks { get; private set; }
    public Rate Percents
    {
        get =>
            new Rate
            {
                One =
                    One * 1 * 100
                    / (1 * (int)One + 2 * (int)Two + 3 * (int)Three + 4 * Four + 5 * Five),
                Two =
                    Two * 2 * 100
                    / (1 * (int)One + 2 * (int)Two + 3 * (int)Three + 4 * Four + 5 * Five),
                Three =
                    Three * 3 * 100
                    / (1 * (int)One + 2 * (int)Two + 3 * (int)Three + 4 * Four + 5 * Five),
                Four =
                    Four * 4 * 100
                    / (1 * (int)One + 2 * (int)Two + 3 * (int)Three + 4 * Four + 5 * Five),
                Five =
                    Five * 5 * 100
                    / (1 * (int)One + 2 * (int)Two + 3 * (int)Three + 4 * Four + 5 * Five)
            };
    }

    public int GetAverageRate() =>
        (1 * (int)One + 2 * (int)Two + 3 * (int)Three + 4 * (int)Four + 5 * (int)Five)
        / (int)(One + Two + Three + Four + Five);

    public void AddRate(int type) =>
        _ = type switch
        {
            1 => One++,
            2 => Two++,
            3 => Three++,
            4 => Four++,
            5 => Five++,
            _ => throw new NotImplementedException("More Than 5 or Less 0 Input.")
        };

    public void RemoveRate(int value) =>
        _ = value switch
        {
            1 => One--,
            2 => Two--,
            3 => Three--,
            4 => Four--,
            5 => Five--,
            _ => throw new NotImplementedException("More Than 5 or Less 0 Input.")
        };

    public Product Product { get; set; }
}
