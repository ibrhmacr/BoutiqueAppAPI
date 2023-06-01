using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.Address.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, CreateAddressCommandResponse>
{
    private readonly IAddressService _addressService;

    public CreateAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }
    
    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        await _addressService.CreateAddressAsync(new()
        {
            AddressName = request.AddressName,
            Description = request.Description
        });
        return new();
    }
}