using Application.Repositories.Cart;
using Persistence.Contexts;

namespace Persistence.Repositories.Cart;

public class CartWriteRepository : WriteRepository<Domain.Entities.Cart>, ICartWriteRepository
{
    public CartWriteRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}