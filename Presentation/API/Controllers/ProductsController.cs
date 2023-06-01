using Application.Abstractions;
using Application.Features.Commands.Product.Create;
using Application.Features.Commands.Product.UploadImage;
using Application.Repositories.ProductImageFile;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator, IFileService fileService, IProductImageFileWriteRepository imageFileWriteRepository, IStorageService storageService)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
    {
        CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadProductImage([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
    {
        uploadProductImageCommandRequest.Files = Request.Form.Files;
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok(response);
    }
}