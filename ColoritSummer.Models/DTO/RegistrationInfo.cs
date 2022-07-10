using ColoritSummer.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoritSummer.Models.DTO
{
    public class RegistrationInfo : IAuthInfo
    {
        public string OrganizationName { get; set; }
        public string City { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
