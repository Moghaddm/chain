using Chain.Domain.Cart.Entities;
using Chain.Domain.Core.Entities;
using System;

namespace Chain.Domain.Cart.Entities;
public class Order : Entity
{
    public DateTimeOffset DateOrdered { get; private set; }
    public Price Price { get; private set; }
    public int Quantity { get; private set; }
    public int LimitOrder { get; private set; }
    public bool IsEmpty { get => Product.Quantity == 0; }
    public Order(DateTimeOffset dateOrdered, Price price, int quantity, int limitOrder)
    {
       
    }
    public Task AddOrder(DateTimeOffset dateOrdered, Price price, int limitOrder)
    {
        if (limitOrder < 1)
            throw new ArgumentOutOfRangeException("The Limit Is A Negative Or Zero.");
        if (dateOrdered.Year > DateTime.UtcNow.Year)
            throw new ArgumentException("Year of Date is Not Valid.");
        DateOrdered = dateOrdered;
        Price = price;
        Quantity++;
        LimitOrder = limitOrder;
        Task.CompletedTask;
    }

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