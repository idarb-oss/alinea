using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Alinea.Core.EventSourcing;

namespace Alinea.TenantSystem.Domain
{
    public interface IRepositoryQuery<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        /// <summary>
        /// Retrive the given aggregate root with a given id from the backend store.
        /// </summary>
        /// <param name="id">The id of the aggregate root</param>
        /// <param name="token">Cancellation Token if operation should be cancelled</param>
        /// <typeparam name="TId">Id type the aggregate root is using</typeparam>
        /// <returns>The aggregate root if any is found</returns>
         Task<TAggregateRoot> GetByIdAsync<TId>(TId id, CancellationToken token = default);

        /// <summary>
        /// Retrives all aggregate roots from the backend store.
        /// </summary>
        /// <param name="token">Cancellation Token if operation should be cancelled</param>
        /// <returns>All aggregate roots in the storage</returns>
         Task<IEnumerable<TAggregateRoot>> GetAllAsync(CancellationToken token = default);
    }
}