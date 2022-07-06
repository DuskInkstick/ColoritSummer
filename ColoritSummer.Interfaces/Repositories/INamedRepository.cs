using ColoritSummer.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColoritSummer.Interfaces.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T : INamedEntity
    {
        Task<T> GetByName(string name, CancellationToken cancel = default);
        Task<T> DeleteByName(string name, CancellationToken cancel = default);
        Task<bool> ExistName(string name, CancellationToken cancel = default);
    }
}
