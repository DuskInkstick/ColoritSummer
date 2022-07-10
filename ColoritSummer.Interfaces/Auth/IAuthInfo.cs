using System;
using System.Collections.Generic;
using System.Text;

namespace ColoritSummer.Interfaces.Auth
{
    public interface IAuthInfo
    {
        string Login { get; set; }
        string Password { get; set; }
    }
}
