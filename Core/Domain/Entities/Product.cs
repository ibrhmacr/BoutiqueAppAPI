using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public int SubCategoryId { get; set; }
    
    public string Name { get; set; }

    public int UnitsInStock { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public SubCategory SubCategory { get; set; }

    public ICollection<ProductImageFile> ProductImageFiles { get; set; }
    
}