using Domain.Entities.Common;

namespace Domain.Entities;

public class Address : BaseEntity
{
    public int UserId { get; set; }

    public AppUser User { get; set; }

    public string AddressName { get; set; }

    public string Description { get; set; }

    public ICollection<Order> Orders { get; set; }
}