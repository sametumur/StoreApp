using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int? CategoryId { get; set; }
    public bool Showcase { get; set; }
    public Category? Category { get; set; }
}
