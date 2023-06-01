using Domain.Entities.Common;

namespace Domain.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }

    public AppUser User { get; set; }

    public ICollection<CartItem> CartItems { get; set; }
    public Order Order { get; set; }

}