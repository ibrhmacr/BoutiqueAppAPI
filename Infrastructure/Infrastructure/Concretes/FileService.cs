using Application.Abstractions;
using Application.DTOs;
using Application.Repositories.Category;
using Application.Repositories.Product;
using Application.Repositories.SubCategory;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Concretes;

public class FileService : IFileService
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly ISubCategoryReadRepository _subCategoryReadRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public FileService(IProductReadRepository productReadRepository, ISubCategoryReadRepository subCategoryReadRepository,
        ICategoryReadRepository categoryReadRepository)
    {
        _productReadRepository = productReadRepository;
        _subCategoryReadRepository = subCategoryReadRepository;
        _categoryReadRepository = categoryReadRepository;
    }
    public async Task<ProductDirectoryDTO> CreateFileDirectoryAsync(int productId)
    {
        var data = from p in _productReadRepository.Table
            join sc in _subCategoryReadRepository.Table on p.SubCategoryId equals sc.Id
            join c in _categoryReadRepository.Table on sc.CategoryId equals c.Id into names
            from n in names
            select new
            {
                Id = p.Id,
                ProductName = p.Name,
                SubCategoryName = sc.Name,
                CategoryName = n.Name
            };
        var result = await data.FirstOrDefaultAsync(p => p.Id == productId);
        return new()
        {
            CategoryName = result.CategoryName,
            SubCategoryName = result.SubCategoryName,
            ProductName = result.ProductName
        };
    }
}