using MediatR;

namespace Application.Features.Queries.Order.GetById;

public class GetOrderByIdQueryRequest : IRequest<GetOrderByIdQueryResponse>
{
    public int OrderId { get; set; }
}