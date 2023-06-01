using Application.Repositories.CartItem;
using Persistence.Contexts;

namespace Persistence.Repositories.CartItem;

public class CartItemWriteRepository : WriteRepository<Domain.Entities.CartItem>, ICartItemWriteRepository
{
    public CartItemWriteRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}