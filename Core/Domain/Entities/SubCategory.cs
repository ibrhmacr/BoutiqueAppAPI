using Domain.Entities.Common;

namespace Domain.Entities;

public class SubCategory : BaseEntity
{
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public Category Category { get; set; }

    public ICollection<Product> Products { get; set; }
}