using Application.Repositories.Order;
using Persistence.Contexts;

namespace Persistence.Repositories.Order;

public class OrderWriteRepository : WriteRepository<Domain.Entities.Common.Order>, IOrderWriteRepository
{
    public OrderWriteRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}