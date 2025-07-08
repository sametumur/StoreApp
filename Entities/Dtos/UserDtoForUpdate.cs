using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record UserDtoForUpdate() : UserDto()
{
    public string Id { get; init; } = string.Empty;
    
    public HashSet<string> UserRoles { get; set; } = new HashSet<string>();
    
    public HashSet<string> AllRoles { get; set; } = new HashSet<string>();

}