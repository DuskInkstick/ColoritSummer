using ColoritSummer.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoritSummer.Models.DTO
{
    public class LoginInfo : IAuthInfo
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
