using Application.Abstractions;
using Application.Repositories.Product;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {

        await _productService.CreateProductAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            SubCategoryId = request.SubCategoryId,
            UnitsInStock = request.UnitsInStock,
            Description = request.Description
        });
        return new();
    }
}