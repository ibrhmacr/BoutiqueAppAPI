using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser<int>
{
    public ICollection<Cart> Carts { get; set; }

    public ICollection<Address> Addresses { get; set; }
}