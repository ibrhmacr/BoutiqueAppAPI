using Application.Repositories.Cart;
using Persistence.Contexts;

namespace Persistence.Repositories.Cart;

public class CartReadRepository : ReadRepository<Domain.Entities.Cart>, ICartReadRepository
{
    public CartReadRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}