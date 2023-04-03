using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.Product.UploadImage;

public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
{
    public int ProductId { get; set; }

    public IFormFileCollection? Files { get; set; }
}