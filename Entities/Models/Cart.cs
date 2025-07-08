namespace Entities.Models;

public class Cart
{
    public int Id { get; set; }
    
    
    public List<CartLine>? CartLines { get; set; }
    
    public Cart()
    {
        CartLines = new List<CartLine>();    
    }

    public virtual void AddProduct(Product product, int quantity)
    {
        CartLine? line = CartLines.Where(x => x.Product.Id == product.Id).FirstOrDefault();
        if (line is null)
        {
            CartLines.Add(new CartLine()
            {
                Product = product,
                Quantity = quantity,
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }
    
    public virtual void RemoveProduct(Product product)
    {
        CartLine? line = CartLines?.Where(x => x.Product.Id == product.Id).FirstOrDefault();
        if (line is not null)
        {
            CartLines?.Remove(line);
        }
    }
    
    public decimal TotalPrice => CartLines.Sum(x => x.Product.Price * x.Quantity);
    
    public int TotalQuantity => CartLines.Sum(x => x.Quantity);
    
    public virtual void Clear() => CartLines.Clear();


}