
using AutoMapper;
using ColoritSummer.Data.MySQL.Entities;
using ColoritSummer.Models.Entities;

namespace ColoritSummer.WebAPI.Infrastructure.Automapper
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserInfo>().ReverseMap();
        }
    }
}
