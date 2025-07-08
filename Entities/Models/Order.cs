using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Order
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Name is required.")]
    public string? Name { get; set; } = string.Empty;
    [Required(ErrorMessage ="Address is required.")]
    public string? Address { get; set; } = string.Empty;
    [Required(ErrorMessage ="Email is required.")]
    public string? Email { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public bool GiftWrap { get; set; }
    public bool Shipped { get; set; }
    public DateTime OrderAt { get; set; } = DateTime.Now;
    public ICollection<CartLine>? CartLines { get; set; } = new List<CartLine>();

}