using Application.Abstractions;
using Application.Repositories.ProductImageFile;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Concretes;

public class ProductService : IProductService
{
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _imageFileWriteRepository;

    public ProductService(IStorageService storageService, IProductImageFileWriteRepository imageFileWriteRepository)
    {
        _storageService = storageService;
        _imageFileWriteRepository = imageFileWriteRepository;
    }

    public async Task<bool> UploadProductImage(int productId, IFormFileCollection files)
    {
        var result = await _storageService.UploadAsync(productId,files);
        bool succeded = await _imageFileWriteRepository.AddRangeAsync(result.Select(p => new ProductImageFile()
        {
            Name = p.fileName,
            Path = p.path,
            ProductId = productId,
        }).ToList());
        if (succeded)
        {
            await _imageFileWriteRepository.SaveAsync();
            return succeded;
        }
        else
            throw new Exception("Urun fotosu eklenirken hata olustu");
        
        
    }
}