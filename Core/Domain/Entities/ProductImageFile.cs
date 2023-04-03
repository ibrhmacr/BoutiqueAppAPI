using Domain.Entities.Common;

namespace Domain.Entities;

public class ProductImageFile : BaseEntity
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public Product Product { get; set; }
}