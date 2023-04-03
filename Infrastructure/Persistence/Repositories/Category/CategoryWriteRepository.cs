using Application.Repositories.Category;
using Persistence.Contexts;

namespace Persistence.Repositories.Category;

public class CategoryWriteRepository : WriteRepository<Domain.Entities.Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}