using CL7Logger.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Repositories
{
    internal abstract class BaseRepository<T> where T : BaseEntity
    {
        public string ConnectionString { get; private set; }

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public abstract Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
