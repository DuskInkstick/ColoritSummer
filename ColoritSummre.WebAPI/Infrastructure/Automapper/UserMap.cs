using AutoMapper;
using ColoritSummer.Data.MySQL.Entities;
using ColoritSummer.Models.DTO;
using ColoritSummer.Models.Entities;

namespace ColoritSummer.WebAPI.Infrastructure.Automapper
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserInfo>().ReverseMap();
            CreateMap<RegistrationInfo, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Login));
        }
    }
}
