using MediatR;

namespace Application.Features.Commands.Product.Create;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public int SubCategoryId { get; set; }
    
    public string Name { get; set; }

    public int UnitsInStock { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }
}