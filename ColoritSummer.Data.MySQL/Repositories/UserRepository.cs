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
    public class UserRepository<T> : Repository<T>, IUserRepository<T> where T : UserEntity, new()
    {
        public UserRepository(ColoritSummerDbContext context) : base(context) { }

        public async Task<T> DeleteByEmail(string email, CancellationToken cancel = default)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var user = Set.Local.FirstOrDefault(u => u.Email == email);
            if (user == null)
                user = await Set.FirstOrDefaultAsync(u => u.Email == email, cancel).ConfigureAwait(false);

            return user;
        }

        public async Task<bool> ExistEmail(string email, CancellationToken cancel = default)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (Set.Local.FirstOrDefault(u => u.Email == email) != null)
                return true;

            return await Set
                .Select(u => new { Email = u.Email })
                .FirstOrDefaultAsync(u => u.Email == email, cancel).ConfigureAwait(false) != null;
        }

        public async Task<T> GetByEmail(string email, CancellationToken cancel = default)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var user = Set.Local.FirstOrDefault(u => u.Email == email);
            if (user != null)
                return user;

            return await Set.FirstOrDefaultAsync(u => u.Email == email, cancel).ConfigureAwait(false);
        }
    }
}
