using Application.DTOs.Order;

namespace Application.Abstractions;

public interface IOrderService
{
    Task<CreateOrderUserDTO> CreateOrderAsync(CreateOrderDTO createOrder);

    List<OrdersDTO> GetUserOrdersAsync();

    Task<OrderDetailsDTO> GetOrderByIdAsync(int orderId);
    
}