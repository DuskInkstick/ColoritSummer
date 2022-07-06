using ColoritSummer.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColoritSummer.Interfaces.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<int> GetCount(CancellationToken cancel = default);

        Task<IEnumerable<T>> GetAll(CancellationToken cancel = default);

        Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default);

        Task<T> GetById(int id, CancellationToken cancel = default);

        Task<T> Add(T item, CancellationToken cancel = default);

        Task<T> Update(T item, CancellationToken cancel = default);

        Task<T> DeleteById(int id, CancellationToken cancel = default);
    }
}
