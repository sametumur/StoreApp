using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record ResetPasswordDto() 
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; } = string.Empty; 
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Confirm Password is required")]   
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; init; } = string.Empty; 

}