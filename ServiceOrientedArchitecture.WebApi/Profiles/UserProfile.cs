using AutoMapper;
using ServiceOrientedArchitecture.Common.Models;
using ServiceOrientedArchitecture.Data.Entities;

namespace SoftwareEngineering.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    { 
        CreateMap<UserEntity, UserDto>()
            .ConstructUsing(src => new UserDto($"{src.FirstName} {src.LastName}", src.Balance));

        CreateMap<UserEntity, UserEntityDto>();
    }
}
