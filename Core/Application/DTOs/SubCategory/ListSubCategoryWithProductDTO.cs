using Domain.Entities;

namespace Application.DTOs.SubCategory;

public class ListSubCategoryWithProductDTO
{
    public string CategoryName { get; set; }
    
    public string SubCategoryName { get; set; }

    public object Products { get; set; }
}