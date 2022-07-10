using ColoritSummer.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoritSummer.Models.Entities
{
    public class UserInfo : IUserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string OrganizationName { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Login:{Email}, Desc:{Description}, Password:{Password}, " +
                $"OrganizationName:{OrganizationName}, City:{City}";
        }
    }
}
