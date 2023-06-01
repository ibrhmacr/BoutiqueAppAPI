using Application.Abstractions;
using Application.DTOs;
using Application.Repositories.Product;
using Application.Repositories.ProductImageFile;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Concretes;

public class ProductService : IProductService
{
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _imageFileWriteRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public ProductService(IStorageService storageService, IProductImageFileWriteRepository imageFileWriteRepository, IProductWriteRepository productWriteRepository)
    {
        _storageService = storageService;
        _imageFileWriteRepository = imageFileWriteRepository;
        _productWriteRepository = productWriteRepository;
    }
    
    public async Task<bool> UploadProductImageAsync(int productId, IFormFileCollection files)
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

    public async Task CreateProductAsync(CreateProductDTO createProduct)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = createProduct.Name,
            SubCategoryId = createProduct.SubCategoryId,
            Description = createProduct.Description,
            UnitsInStock = createProduct.UnitsInStock,
            Price = createProduct.Price
        });
        await _productWriteRepository.SaveAsync();
    }
}