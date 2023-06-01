using Application.Repositories.Order;
using Persistence.Contexts;

namespace Persistence.Repositories.Order;

public class OrderReadRepository : ReadRepository<Domain.Entities.Common.Order>, IOrderReadRepository
{
    public OrderReadRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}