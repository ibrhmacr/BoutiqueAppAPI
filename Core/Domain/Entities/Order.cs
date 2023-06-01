namespace Domain.Entities.Common;

public class Order : BaseEntity
{
    public string Username { get; set; }
    public int AddressId { get; set; }
    
    public Address Address { get; set; }
    public Cart Cart { get; set; }
    public string OrderCode { get; set; }

}