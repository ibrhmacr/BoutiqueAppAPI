namespace Application.Features.Queries.Category.GetSubcategoriesWithProducts;

public class GetSubcategoriesWithProductsQueryResponse
{
    public string CategoryName { get; set; }
    
    public string SubCategoryName { get; set; }

    public object Products { get; set; }
}