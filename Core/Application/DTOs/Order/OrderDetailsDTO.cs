using Domain.Entities;

namespace Application.DTOs.Order;

public class OrderDetailsDTO
{
    public string OrderCode { get; set; }

    public int TotalCartItemCount { get; set; }

    public object CartItems { get; set; }

    public string AddressName { get; set; }

    public string AddressDescription { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }
}