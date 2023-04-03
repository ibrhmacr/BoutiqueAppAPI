using Domain.Entities;

namespace Application.DTOs.SubCategory;

public class ListSubCategoryWithProductDTO
{
    public string SubCategoryName { get; set; }

    public List<Product> Products { get; set; }
}