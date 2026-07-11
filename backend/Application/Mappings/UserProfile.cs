using AutoMapper;
using Application.DTOs.Users;
using Domain.Entities;
using Application.DTOs.Jobs;
//using Domain.Entities;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<CreateUserDto, User>();

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<User, UpdateUserDto>();

        CreateMap<Job, JobResponseDto>()
    .ForMember(
        dest => dest.Status,
        opt => opt.MapFrom(src => src.Status.ToString()));
    }
}