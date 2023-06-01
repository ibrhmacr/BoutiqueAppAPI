using Application.Abstractions;
using MediatR;

namespace Application.Features.Queries.Order.GetAll;

public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQueryRequest, List<GetUserOrdersQueryResponse>>
{
    private readonly IOrderService _orderService;

    public GetUserOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<List<GetUserOrdersQueryResponse>> Handle(GetUserOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var orders = _orderService.GetUserOrdersAsync();
        return orders.Select(u => new GetUserOrdersQueryResponse()
        {
            CreatedDate = u.CreatedDate,
            DeliveryDate = u.DeliveryDate,
            Username = u.Username,
            OrderCode = u.OrderCode,
            TotalPrice = u.TotalPrice
        }).ToList();
    }
}