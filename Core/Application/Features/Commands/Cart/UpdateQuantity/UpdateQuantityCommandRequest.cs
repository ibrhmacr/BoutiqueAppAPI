using MediatR;

namespace Application.Features.Commands.Cart.UpdateQuantity;

public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
{
    public int BasketItemId { get; set; }
    
    public int Quantity { get; set; }
}