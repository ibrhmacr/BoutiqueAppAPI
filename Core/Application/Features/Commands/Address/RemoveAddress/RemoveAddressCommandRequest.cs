using MediatR;

namespace Application.Features.Commands.Address.RemoveAddress;

public class RemoveAddressCommandRequest : IRequest<RemoveAddressCommandResponse>
{
    public int AddressId { get; set; }
}