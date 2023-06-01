using Domain.Entities.Common;

namespace Domain.Entities;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }

    public int ProductId { get; set; }

    public Cart Cart { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }
    
}