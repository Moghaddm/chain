using Chain.Domain.Cart.Entities;
using Chain.Domain.Core.Entities;

namespace Chain.Domain.Cart.Entities;
public class Order : Entity
{
    public Product Product { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public long Price { get; set; }
    public int Quantity { get; set; }
    public int Limite { get; set; }
}