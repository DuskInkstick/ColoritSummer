using ColoritSummer.Data.MySQL.Context;
using ColoritSummer.Data.MySQL.Entities.Base;
using ColoritSummer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColoritSummer.Data.MySQL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly ColoritSummerDbContext _db;
        protected DbSet<T> Set { get; }

        public DbRepository(ColoritSummerDbContext context)
        {
            _db = context;
            Set = _db.Set<T>();
        }

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await _db.AddAsync(item, cancel).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return item;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Id == id);

            if (item == null)
                item = await Set
                   .Select(i => new T { Id = i.Id })
                   .FirstOrDefaultAsync(i => i.Id == id, cancel)
                   .ConfigureAwait(false);

            if (item == null)
                return null;

            _db.Remove(item);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return item;
        }

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default)
        {
            if (count <= 0)
                return Enumerable.Empty<T>();

            IQueryable<T> query = Set.OrderBy(i => i.Id);

            if (skip > 0)
                query = query.Skip(skip);

            return await query.Take(count).ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            return await Set.ToArrayAsync(cancel).ConfigureAwait(false);    
        }

        public async Task<T> GetById(int id, CancellationToken cancel = default)
        {
            return await Set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false);  
        }

        public async Task<int> GetCount(CancellationToken cancel = default)
        {
           return await Set.CountAsync(cancel).ConfigureAwait(false);
        }

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Update(item);

            return item;
        }
    }
}
