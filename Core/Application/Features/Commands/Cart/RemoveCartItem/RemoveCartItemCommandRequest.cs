using MediatR;

namespace Application.Features.Commands.Cart.RemoveCartItem;

public class RemoveCartItemCommandRequest : IRequest<RemoveCartItemCommandResponse>
{
    public int CartItemId { get; set; }
}