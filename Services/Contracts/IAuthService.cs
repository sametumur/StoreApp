using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthService
{
    IEnumerable<IdentityRole> Roles {get; }
    IEnumerable<IdentityRole> GetAllRoles(bool trackChanges);
    IdentityRole? GetRole(string id, bool trackChanges);
    void CreateRole(RoleDtoForInsertion roleDto);
    void UpdateRole(RoleDtoForUpdate role);
    void DeleteRole(string id);
    RoleDtoForUpdate GetRoleForUpdate(string id, bool trackChanges);
    
    IEnumerable<IdentityUser> GetAllUsers(bool trackChanges);
    Task<IdentityUser> GetUser(string id, bool trackChanges);
    Task<IdentityResult>  CreateUser(UserDtoForInsertion userDto);
    Task UpdateUser(UserDtoForUpdate user);
    
    Task<IdentityResult>  ResetPassword(ResetPasswordDto resetPasswordDto);
    void DeleteUser(string id);
    Task<UserDtoForUpdate> GetUserForUpdate(string id, bool trackChanges);
}