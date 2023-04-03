using Application.Repositories.Product;
using Persistence.Contexts;

namespace Persistence.Repositories.Product;

public class ProductReadRepository : ReadRepository<Domain.Entities.Product>, IProductReadRepository
{
    public ProductReadRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}