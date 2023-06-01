using Application.Abstractions;
using Application.DTOs.Order;
using MediatR;

namespace Application.Features.Queries.Order.GetById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
{
    private readonly IOrderService _orderService;

    public GetOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrderByIdAsync(request.OrderId);
        return new()
        {
            TotalPrice = result.TotalPrice,
            CartItems = result.CartItems,
            CreatedDate = result.CreatedDate,
            TotalCartItemCount = result.TotalCartItemCount,
            OrderCode = result.OrderCode,
            AddressName = result.AddressName,
            AddressDescription = result.AddressDescription
        };
    }
}