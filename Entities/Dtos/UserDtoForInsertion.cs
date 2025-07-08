using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record UserDtoForInsertion() : UserDto()
{
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; } = string.Empty; 

}