using Application.Features.Queries.Category.GetSubcategoriesWithProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : Controller
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubcategoriesWithProductsAsync([FromQuery] GetSubcategoriesWithProductsQueryRequest getSubcategoriesWithProductsQueryRequest)
    {
        List<GetSubcategoriesWithProductsQueryResponse> responses =
            await _mediator.Send(getSubcategoriesWithProductsQueryRequest);
        return Ok(responses);
    }
}