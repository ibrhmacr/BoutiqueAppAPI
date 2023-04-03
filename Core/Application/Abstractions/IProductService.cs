using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface IProductService
{
    Task<bool> UploadProductImage(int productId, IFormFileCollection files);
}