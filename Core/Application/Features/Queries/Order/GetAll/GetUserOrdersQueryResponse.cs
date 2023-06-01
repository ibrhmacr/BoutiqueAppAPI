namespace Application.Features.Queries.Order.GetAll;

public class GetUserOrdersQueryResponse
{
    public string Username { get; set; }

    public decimal TotalPrice { get; set; }

    public string OrderCode { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime DeliveryDate { get; set; }
}