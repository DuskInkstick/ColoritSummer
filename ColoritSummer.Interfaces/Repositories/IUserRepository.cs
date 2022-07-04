using ColoritSummer.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColoritSummer.Interfaces.Repositories
{
    public interface IUserRepository<T> : IRepository<T> where T : IUserEntity 
    {
        Task<T> GetByEmail(string email, CancellationToken cancel = default);
        Task<T> DeleteByEmail(string email, CancellationToken cancel = default);
        Task<bool> ExistEmail(string email, CancellationToken cancel = default);
    }
}
