using Application.Features.Commands.Cart.AddCartItemToCart;
using Application.Features.Commands.Cart.RemoveCartItem;
using Application.Features.Commands.Cart.UpdateQuantity;
using Application.Features.Queries.Cart.GetAllCartItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "User")]
public class CartsController : Controller
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItemToCart([FromBody] AddCartItemToCartCommandRequest addCartItemToCartCommandRequest)
    {
        AddCartItemToCartCommandResponse response = await _mediator.Send(addCartItemToCartCommandRequest);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
    {
        UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveCartItem(RemoveCartItemCommandRequest removeCartItemCommandRequest)
    {
        RemoveCartItemCommandResponse response = await _mediator.Send(removeCartItemCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCartItems(GetAllCartItemQueryRequest getAllCartItemQueryRequest)
    {
        List<GetAllCartItemQueryResponse> responses = await _mediator.Send(getAllCartItemQueryRequest);
        return Ok(responses);
    }

}