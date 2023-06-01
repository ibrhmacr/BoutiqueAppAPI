using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.Address.RemoveAddress;

public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommandRequest,RemoveAddressCommandResponse>
{
    private readonly IAddressService _addressService;

    public RemoveAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }
    
    public async Task<RemoveAddressCommandResponse> Handle(RemoveAddressCommandRequest request, CancellationToken cancellationToken)
    {
        await _addressService.RemoveAddressAsync(request.AddressId);
        return new();
    }
}