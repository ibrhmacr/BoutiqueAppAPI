using Application.Repositories.Address;
using Persistence.Contexts;

namespace Persistence.Repositories.Address;

public class AddressWriteRepository : WriteRepository<Domain.Entities.Address>, IAddressWriteRepository
{
    public AddressWriteRepository(BoutiqueAppAPIDbContext context) : base(context)
    {
    }
}