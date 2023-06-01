using MediatR;

namespace Application.Features.Queries.Address.GetById;

public class GetAddressByIdQueryRequest : IRequest<GetAddressByIdQueryResponse>
{
    public int AddressId { get; set; }
}