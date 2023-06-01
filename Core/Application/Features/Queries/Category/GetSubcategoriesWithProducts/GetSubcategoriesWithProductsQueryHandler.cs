using Application.Abstractions;
using MediatR;

namespace Application.Features.Queries.Category.GetSubcategoriesWithProducts;

public class GetSubcategoriesWithProductsHandler : IRequestHandler<GetSubcategoriesWithProductsQueryRequest, List<GetSubcategoriesWithProductsQueryResponse>>
{
    private readonly ICategoryService _categoryService;

    public GetSubcategoriesWithProductsHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<List<GetSubcategoriesWithProductsQueryResponse>> Handle(GetSubcategoriesWithProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetSubcategoriesWithProductsAsync();
        return result.Select(p => new GetSubcategoriesWithProductsQueryResponse()
        {
            CategoryName = p.CategoryName,
            SubCategoryName = p.SubCategoryName,
            Products = p.Products
        }).ToList();


    }
}