using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface IProductService
{
    Task<bool> UploadProductImageAsync(int productId, IFormFileCollection files);

    Task CreateProductAsync(CreateProductDTO createProduct);
}