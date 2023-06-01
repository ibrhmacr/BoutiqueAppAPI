using Application.Abstractions;
using Application.Constants;
using Application.DTOs.Order;
using MediatR;

namespace Application.Features.Commands.Order.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly IMailService _mailService;
    private readonly ICartService _cartService;

    public CreateOrderCommandHandler(IOrderService orderService, IMailService mailService, ICartService cartService)
    {
        _orderService = orderService;
        _mailService = mailService;
        _cartService = cartService;
    }
    
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        CreateOrderUserDTO createOrderUserDto = await _orderService.CreateOrderAsync(new()
        {
            AddressId = request.AddressId,
            BasketId = _cartService.GetUserActiveBasket.Id,
        });
        _mailService.SendCreateOrderMailAsync(createOrderUserDto.Email,createOrderUserDto.OrderCode,createOrderUserDto.Username, createOrderUserDto.AddressName);
        return new()
        {
            Message = Messages.OrderCreatedSuccesfully
        };
    }
}