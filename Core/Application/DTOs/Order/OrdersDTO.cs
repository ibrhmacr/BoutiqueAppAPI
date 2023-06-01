namespace Application.DTOs.Order;

public class OrdersDTO
{
    
    public string Username { get; set; }

    public decimal TotalPrice { get; set; }

    public string OrderCode { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime DeliveryDate { get; set; }
    
}