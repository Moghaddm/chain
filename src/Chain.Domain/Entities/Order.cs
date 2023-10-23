using System;

namespace Chain.Domain.Entities;

public sealed class Order : Entity
{
    public DateTimeOffset DateOrdered { get; private set; }
    public Price Price { get; private set; }
    public int Quantity { get; private set; }
    public int LimitOrder { get; private set; }
    public bool IsEmpty
    {
        get => Product.Quantity == 0;
    }

    public async Task AddOrder(DateTimeOffset dateOrdered, Price price, int limitOrder)
    {
        if (limitOrder < 1)
            throw new ArgumentOutOfRangeException("The Limit Is A Negative Or Zero.");
        if (dateOrdered.Year > DateTime.UtcNow.Year)
            throw new ArgumentException("Year of Date is Not Valid.");

        DateOrdered = dateOrdered;
        Price = price;
        Quantity = 1;
        LimitOrder = limitOrder;
        await Task.CompletedTask;
    }

    public async Task AddQuantity() => Quantity++;

    public bool CanAddQuantity(int countProducts)
    {
        if (IsEmpty)
            return false;
        if (Quantity + 1 >= Product.Category.LimitOrder)
            return false;
        return true;
    }

    public Product Product { get; }
}
