using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Entities.Dtos;

public record ProductDto
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; init; }
    
    public string? Description { get; init; } = string.Empty;
    
    public string? ImageUrl { get; set; }
    
    public int? CategoryId { get; init; }
}