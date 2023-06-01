namespace Application.DTOs;

public class CreateProductDTO
{
    public int SubCategoryId { get; set; }
    
    public string Name { get; set; }

    public int UnitsInStock { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }
}