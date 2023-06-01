using Domain.Entities;

namespace Application.Features.Queries.Order.GetById;

public class GetOrderByIdQueryResponse
{
    public string OrderCode { get; set; }

    public int TotalCartItemCount { get; set; }

    public object CartItems { get; set; }

    public string AddressName { get; set; }

    public string AddressDescription { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }
}