using Application.Features.Commands.Address.CreateAddress;
using Application.Features.Commands.Address.RemoveAddress;
using Application.Features.Queries.Address.GetAll;
using Application.Features.Queries.Address.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "User")]
public class AddressesController : Controller
{
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody]CreateAddressCommandRequest createAddressCommandRequest)
    {
        CreateAddressCommandResponse response = await _mediator.Send(createAddressCommandRequest);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAddress(RemoveAddressCommandRequest removeAddressCommandRequest)
    {
        RemoveAddressCommandResponse response = await _mediator.Send(removeAddressCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAddresses([FromRoute]GetUserAddressesQueryRequest getUserAddressesQueryRequest)
    {
        List<GetUserAddressesQueryResponse> response = await _mediator.Send(getUserAddressesQueryRequest);
        return Ok(response);
        
    }

    [HttpGet("{AddressId}")]
    public async Task<IActionResult> GetAddressById(GetAddressByIdQueryRequest getAddressByIdQueryRequest)
    {
        GetAddressByIdQueryResponse response = await _mediator.Send(getAddressByIdQueryRequest);
        return Ok(response);
    }
}