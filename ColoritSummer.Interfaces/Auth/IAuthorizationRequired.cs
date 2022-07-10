using System;
using System.Collections.Generic;
using System.Text;

namespace ColoritSummer.Interfaces.Auth
{
    public interface IAuthorizationRequired
    {
        void SetAccesToken(string token);
    }
}
