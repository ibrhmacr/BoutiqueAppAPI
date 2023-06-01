using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.Cart.UpdateQuantity;

public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
{
    private readonly ICartService _cartService;

    public UpdateQuantityCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
    {
        await _cartService.UpdateQuantityAsync(new()
        {
            BasketItemId = request.BasketItemId,
            Quantity = request.Quantity
        });
        return new();
    }
}