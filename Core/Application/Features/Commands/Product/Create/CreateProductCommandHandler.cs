using Application.Repositories.Product;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            SubCategoryId = request.SubCategoryId,
            UnitsInStock = request.UnitsInStock,
            Description = request.Description,
        });
        await _productWriteRepository.SaveAsync();
        return new();
    }
}