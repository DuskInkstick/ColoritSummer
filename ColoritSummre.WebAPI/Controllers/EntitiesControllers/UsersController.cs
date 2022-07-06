using AutoMapper;
using ColoritSummer.Data.MySQL.Entities;
using ColoritSummer.Interfaces.Repositories;
using ColoritSummer.Models.Entities;
using ColoritSummer.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColoritSummer.WebAPI.Controllers.EntitiesControllers
{
    public class UsersController : MappedEntityController<UserInfo, User>
    {
        public UsersController(IRepository<User> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
