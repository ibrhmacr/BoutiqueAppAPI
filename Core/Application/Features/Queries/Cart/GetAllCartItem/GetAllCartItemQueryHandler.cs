using Application.Abstractions;
using MediatR;

namespace Application.Features.Queries.Cart.GetAllCartItem;

public class GetAllCartItemQueryHandler : IRequestHandler<GetAllCartItemQueryRequest, List<GetAllCartItemQueryResponse>>
{
    private readonly ICartService _cartService;

    public GetAllCartItemQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<List<GetAllCartItemQueryResponse>> Handle(GetAllCartItemQueryRequest request, CancellationToken cancellationToken)
    {
        var cartItems = await _cartService.GetAllCartItemAsync();
        return cartItems.Select(ca => new GetAllCartItemQueryResponse()
        {
            BasketItemId = ca.Id,
            Name = ca.Product.Name,
            Price = ca.Product.Price,
            Quantity = ca.Quantity
        }).ToList();
    }
}