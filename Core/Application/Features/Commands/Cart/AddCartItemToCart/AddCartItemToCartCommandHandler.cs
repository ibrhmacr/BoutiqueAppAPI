using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.Cart.AddCartItemToCart;

public class AddCartItemToCartCommandHandler : IRequestHandler<AddCartItemToCartCommandRequest, AddCartItemToCartCommandResponse>
{
    private readonly ICartService _cartService;

    public AddCartItemToCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<AddCartItemToCartCommandResponse> Handle(AddCartItemToCartCommandRequest request, CancellationToken cancellationToken)
    {
        await _cartService.AddCartItemToCartAsync(new()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });
        return new();
    }
}