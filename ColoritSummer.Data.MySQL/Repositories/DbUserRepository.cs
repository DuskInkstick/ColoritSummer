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
    public class DbUserRepository<T> : DbRepository<T>, IUserRepository<T> where T : UserEntity, new()
    {
        public DbUserRepository(ColoritSummerDbContext context) : base(context) { }

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

            return await Set.AnyAsync(cancel).ConfigureAwait(false);
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
