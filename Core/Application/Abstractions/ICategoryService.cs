using Application.DTOs.SubCategory;

namespace Application.Abstractions;

public interface ICategoryService
{
    Task<List<string>> GetCategoryNamesAsync(int categoryId);

    Task<bool> CreateCategoryAsync(string name);

    Task<List<ListSubCategoryWithProductDTO>> GetSubcategoriesWithProducts(int categoryId);
    
    


}