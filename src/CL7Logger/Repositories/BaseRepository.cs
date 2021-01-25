using CLogger.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CLogger.Repositories
{
    internal abstract class BaseRepository<T> where T : BaseEntity
    {
        public string ConnectionString { get; private set; }

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public abstract Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
        public abstract Task<IEnumerable<T>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken);
    }
}
