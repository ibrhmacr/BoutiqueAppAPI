using MediatR;

namespace Application.Features.Commands.Cart.AddCartItemToCart;

public class AddCartItemToCartCommandRequest : IRequest<AddCartItemToCartCommandResponse>
{
    public int ProductId { get; set; }
    
    public int Quantity { get; set; }
}