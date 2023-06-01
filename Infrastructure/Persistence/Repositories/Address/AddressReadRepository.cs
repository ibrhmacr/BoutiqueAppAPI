using Application.Repositories.Address;
using Persistence.Contexts;

namespace Persistence.Repositories.Address;

public class AddressReadRepository : ReadRepository<Domain.Entities.Address>, IAddressReadRepository
{
    public AddressReadRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}