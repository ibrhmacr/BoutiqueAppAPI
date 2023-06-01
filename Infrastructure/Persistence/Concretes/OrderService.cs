using Application.Abstractions;
using Application.DTOs.Order;
using Application.Repositories.Cart;
using Application.Repositories.Order;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Concretes;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IAddressService _addressService;
    private readonly ICartReadRepository _cartReadRepository;
    private readonly ICartWriteRepository _cartWriteRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IOrderReadRepository orderReadRepository, IAddressService addressService, ICartReadRepository cartReadRepository, ICartWriteRepository cartWriteRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _orderReadRepository = orderReadRepository;
        _addressService = addressService;
        _cartReadRepository = cartReadRepository;
        _cartWriteRepository = cartWriteRepository;
    }


    public async Task<CreateOrderUserDTO> CreateOrderAsync(CreateOrderDTO createOrderDto)
    {
        var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        
        var orderCode = (new Random().NextDouble() * 10000).ToString();
        orderCode = orderCode
            .Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

        var result = await _orderWriteRepository.AddAsync(new()
        {
            AddressId = createOrderDto.AddressId,
            Id = createOrderDto.BasketId,
            OrderCode = orderCode,
            Username = user.UserName
        });
        
        await _orderWriteRepository.SaveAsync();
        
        var addressName = await _addressService.GetAddressById(createOrderDto.AddressId);

        if (result)
        {
            
            return new()
            {
                OrderCode = orderCode,
                Email = user.Email,
                Username = user.UserName,
                AddressName = addressName.AddressName
            };
        }
        throw new Exception("When Creating order error occur");
    }

    public List<OrdersDTO> GetUserOrdersAsync()
    {
        var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        
        var orders = _orderReadRepository.GetWhere(o => o.Username == username);

        var data = orders.Select(o => new OrdersDTO()
        {
            OrderCode = o.OrderCode,
            Username = o.Username,
            CreatedDate = o.CreatedDate,
            DeliveryDate = o.CreatedDate.AddDays(5),
            TotalPrice = o.Cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
        }).ToList();

        return data;
    }

    public async Task<OrderDetailsDTO> GetOrderByIdAsync(int orderId)
    {
        var query = _orderReadRepository.Table.Include(o => o.Cart).ThenInclude(c => c.CartItems)
            .ThenInclude(ci => ci.Product);

        var data = await (from order in query
            join cart in _cartReadRepository.Table on order.Id equals cart.Id into OrderedCart
            from a in OrderedCart.DefaultIfEmpty() 
            select new 
            {
                Id = order.Id,
                AddressDescription = order.Address.Description,
                AddressName = order.Address.AddressName,
                Cart = order.Cart,
                OrderCode = order.OrderCode,
                TotalCartItemCount = order.Cart.CartItems.Count(),
                TotalPrice = order.Cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                CreatedDate = order.CreatedDate
            }).FirstOrDefaultAsync(c => c.Id == orderId);
        
        return new()
        {
            AddressName = data.AddressName,
            AddressDescription = data.AddressDescription,
            OrderCode = data.OrderCode,
            TotalCartItemCount = data.TotalCartItemCount,
            TotalPrice = data.TotalPrice,
            CartItems = data.Cart.CartItems.Select(ci => new
            {
                ci.Product.Name,
                ci.Product.Description,
                ci.Quantity
            }),
            CreatedDate = data.CreatedDate
        };
    }
}