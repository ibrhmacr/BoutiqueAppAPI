using Application.Abstractions;
using MediatR;

namespace Application.Features.Queries.Address.GetById;

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQueryRequest, GetAddressByIdQueryResponse>
{
    private readonly IAddressService _addressService;

    public GetAddressByIdQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<GetAddressByIdQueryResponse> Handle(GetAddressByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var address = await _addressService.GetAddressById(request.AddressId);
        return new()
        {
            AddressName = address.AddressName,
            Description = address.Description
        };
    }
}