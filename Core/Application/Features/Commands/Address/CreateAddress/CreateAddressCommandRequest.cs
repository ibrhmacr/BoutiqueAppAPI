using MediatR;

namespace Application.Features.Commands.Address.CreateAddress;

public class CreateAddressCommandRequest : IRequest<CreateAddressCommandResponse>
{
    public string AddressName { get; set; }

    public string Description { get; set; }
}