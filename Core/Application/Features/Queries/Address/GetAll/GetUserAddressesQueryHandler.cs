using Application.Abstractions;
using MediatR;

namespace Application.Features.Queries.Address.GetAll;

public class GetUserAddressesQueryHandler : IRequestHandler<GetUserAddressesQueryRequest, List<GetUserAddressesQueryResponse>>
{
    private readonly IAddressService _addressService;

    public GetUserAddressesQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<List<GetUserAddressesQueryResponse>> Handle(GetUserAddressesQueryRequest request, CancellationToken cancellationToken)
    {
        var addresses = await _addressService.GetUserAddresses();
        return addresses.Select(a => new GetUserAddressesQueryResponse()
        {
            AddressName = a.AddressName,
            Description = a.Description
        }).ToList();
    }
}