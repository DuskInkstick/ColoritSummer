using ColoritSummer.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoritSummer.Data.MySQL.Entities.Base
{
    public class UserEntity : Entity, IUserEntity
    {
        public string Email { get; set; }
    }
}
