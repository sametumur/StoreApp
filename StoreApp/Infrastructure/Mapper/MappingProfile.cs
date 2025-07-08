using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDtoForInsertion, Product>();
        CreateMap<ProductDtoForUpdate, Product>().ReverseMap();
        
        CreateMap<CategoryDtoForInsertion, Category>();
        CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
        
        CreateMap<IdentityRole, RoleDto>();
        CreateMap<IdentityRole, RoleDtoForUpdate>();
        CreateMap<RoleDtoForUpdate, IdentityRole>();
        CreateMap<RoleDtoForInsertion, IdentityRole>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<IdentityUser, RoleDto>();
        CreateMap<IdentityUser, UserDtoForUpdate>();
        CreateMap<UserDtoForUpdate, IdentityUser>();
        CreateMap<UserDtoForInsertion, IdentityUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());



    }
}