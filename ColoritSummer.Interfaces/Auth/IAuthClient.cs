using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColoritSummer.Interfaces.Auth
{
    public interface IAuthClient<TLog, TReg> where TReg : IAuthInfo where TLog : IAuthInfo
    {
        Task<string> Login(TLog info);
        Task<bool> Registration(TReg info); 
    }
}
