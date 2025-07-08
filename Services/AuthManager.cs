using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services;

public class AuthManager : IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;

    public AuthManager(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    public IEnumerable<IdentityRole> Roles => _roleManager.Roles;
    
    
    public IEnumerable<IdentityRole> GetAllRoles(bool trackChanges)
    {
        return _roleManager.Roles;
    }

    public IdentityRole? GetRole(string id, bool trackChanges)
    {
        IdentityRole? role = _roleManager.FindByIdAsync(id).Result;
        if (role == null)
            throw new Exception("Role not found");
        
        return role;
    }
    
    public RoleDtoForUpdate GetRoleForUpdate(string id, bool trackChanges)
    {
        IdentityRole role = _roleManager.FindByIdAsync(id).Result;
        if (role == null)
            throw new Exception("Role not found");
        var roleDto = _mapper.Map<RoleDtoForUpdate>(role);
        return roleDto;
    }

    public void CreateRole(RoleDtoForInsertion roleDto)
    {
        IdentityRole role = _mapper.Map<IdentityRole>(roleDto);
        _roleManager.CreateAsync(role).Wait();
    }

    public void UpdateRole(RoleDtoForUpdate roleDto)
    {
        IdentityRole role = _mapper.Map<IdentityRole>(roleDto);
       _roleManager.UpdateAsync(role).Wait();
    }

    public void DeleteRole(string id)
    {
        IdentityRole? role = _roleManager.FindByIdAsync(id).Result;
        if (role is not null)
        {
           _roleManager.DeleteAsync(role).Wait();
        }
    }
    
    public IEnumerable<IdentityUser> GetAllUsers(bool trackChanges)
    {
        return _userManager.Users.ToList();
    }

    public async Task<IdentityUser> GetUser(string id, bool trackChanges)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");
        return user;
    }
    
    public async Task<UserDtoForUpdate> GetUserForUpdate(string id, bool trackChanges)
    {
        IdentityUser user = await GetUser(id, trackChanges);
       var userDto = _mapper.Map<UserDtoForUpdate>(user);
        userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList());
        userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
        userDto.AllRoles = _roleManager.Roles.Select(r => r.Name).ToHashSet();
        return userDto;
    }

    public async Task<IdentityResult> CreateUser(UserDtoForInsertion userDto)
    {
        IdentityUser user = _mapper.Map<IdentityUser>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception("User creation failed");
        }

        if (userDto.Roles.Count > 0)
        {
            var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
            if (!roleResult.Succeeded)
            {
                throw new Exception("User Role creation failed");
            }
        }
        return result;
    }

    public async Task UpdateUser(UserDtoForUpdate userDto)
    {
        var user = await GetUser(userDto.Id, false);
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("User update failed");
        }

        if (userDto.Roles.Count <= 0) return;
        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRolesAsync(user, userDto.Roles);
    }

    public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await GetUser(resetPasswordDto.Id, false);
        await _userManager.RemovePasswordAsync(user);
        var result = await _userManager.AddPasswordAsync(user, resetPasswordDto.Password);
        return result;
    }

    public void DeleteUser(string id)
    {
        IdentityUser? user = _userManager.FindByIdAsync(id).Result;
        if (user is not null)
        {
            _userManager.DeleteAsync(user).Wait();
        }
    }
}