using Application.Abstractions;
using Application.DTOs.SubCategory;
using Application.Repositories.Category;
using Application.Repositories.Product;
using Application.Repositories.SubCategory;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly ISubCategoryReadRepository _subCategoryReadRepository;
    private readonly IProductReadRepository _productReadRepository;

    public CategoryService(ICategoryReadRepository categoryReadRepository, ISubCategoryReadRepository subCategoryReadRepository, IProductReadRepository productReadRepository)
    {
        _categoryReadRepository = categoryReadRepository;
        _subCategoryReadRepository = subCategoryReadRepository;
        _productReadRepository = productReadRepository;
    }
    public async Task<List<ListSubCategoryWithProductDTO>> GetSubcategoriesWithProductsAsync()
    {
        var query = _categoryReadRepository.Table
            .Include(c => c.SubCategories)
            .ThenInclude(sc => sc.Products).AsQueryable();

        var data = from c in query
            join sc in _subCategoryReadRepository.Table on c.Id equals sc.Id
            join p in _productReadRepository.Table on sc.Id equals p.SubCategoryId into Products
            from a in Products
            select new
            {
                CategoryName = a.SubCategory.Category.Name,
                SubCategoryName = a.SubCategory.Name,
                Products = sc.Products
            };

        return data.Select(d => new ListSubCategoryWithProductDTO
        {
            CategoryName = d.CategoryName,
            SubCategoryName = d.SubCategoryName,
            Products = d.Products.Select(p => new
            {
                ProductName = p.Name,
                ProductDescription = p.Description,
                ProductPrice = p.Price
            }),
        }).ToList();

    }
}