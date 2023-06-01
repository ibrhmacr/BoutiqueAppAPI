namespace Application.Features.Queries.Cart.GetAllCartItem;

public class GetAllCartItemQueryResponse
{
    public int BasketItemId { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    //todo Response da donucek oldugun datalari tekrardan duzenle.
}