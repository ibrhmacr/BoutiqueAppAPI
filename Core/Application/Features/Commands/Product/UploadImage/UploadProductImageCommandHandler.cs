using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.Product.UploadImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    private readonly IProductService _productService;

    public UploadProductImageCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        bool succeded = await _productService.UploadProductImageAsync(request.ProductId, request.Files);
        if (succeded)
            return new();
        else
            throw new Exception("Hata");
        

    }
}