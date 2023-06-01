using Application.Repositories.CartItem;
using Persistence.Contexts;

namespace Persistence.Repositories.CartItem;

public class CartItemReadRepository : ReadRepository<Domain.Entities.CartItem>, ICartItemReadRepository
{
    public CartItemReadRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}