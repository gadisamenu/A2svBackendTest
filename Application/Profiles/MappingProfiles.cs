using AutoMapper;
using Domain;
using Application.Features.Auth.Dtos;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AppUser 
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserDetailDto>();
            CreateMap<RegisterDto, AppUser>();
            #endregion


        }

    }

}

