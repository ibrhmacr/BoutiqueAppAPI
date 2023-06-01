using Application.Features.Commands.Order.CreateOrder;
using Application.Features.Queries.Order.GetAll;
using Application.Features.Queries.Order.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "User")]
public class OrdersController : Controller
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest createOrderCommandRequest)
    {
        CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserOrders([FromQuery] GetUserOrdersQueryRequest getUserOrdersQueryRequest)
    {
        List<GetUserOrdersQueryResponse> response = await _mediator.Send(getUserOrdersQueryRequest);
        return Ok(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetOrderById([FromQuery] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
    {
        GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);
        return Ok(response);
    }
}