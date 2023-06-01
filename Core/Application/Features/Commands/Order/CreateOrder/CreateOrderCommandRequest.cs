using MediatR;

namespace Application.Features.Commands.Order.CreateOrder;

public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
{
    public int AddressId { get; set; }
    
}