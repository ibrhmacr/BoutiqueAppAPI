using Application.Repositories.ProductImageFile;
using Persistence.Contexts;

namespace Persistence.Repositories.ProductImageFile;

public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>,IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}