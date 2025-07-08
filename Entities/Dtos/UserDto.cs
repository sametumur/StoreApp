using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record UserDto()
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; init; } = string.Empty;
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; init; } = string.Empty;
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; init; } = string.Empty;
    public HashSet<string> Roles { get; set; } = new HashSet<string>();
}