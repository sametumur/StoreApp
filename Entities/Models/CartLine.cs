namespace Entities.Models;

public class CartLine
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    public Product Product { get; set; }
}