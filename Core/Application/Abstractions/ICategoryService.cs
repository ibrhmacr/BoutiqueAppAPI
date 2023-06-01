using Application.DTOs.SubCategory;

namespace Application.Abstractions;

public interface ICategoryService
{
    Task<List<ListSubCategoryWithProductDTO>> GetSubcategoriesWithProductsAsync();
}