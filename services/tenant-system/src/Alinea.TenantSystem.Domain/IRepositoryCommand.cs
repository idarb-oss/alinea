using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Alinea.Core.EventSourcing;

namespace Alinea.TenantSystem.Domain
{
    public interface IRepositoryCommand<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        /// <summary>
        /// Store a new aggregate root to the storage backend.
        /// </summary>
        /// <param name="aggregate">The aggregate root to be stored</param>
        /// <param name="token">Cancellation Token if the operation needs to be cancelled.</param>
         Task SaveAsync(TAggregateRoot aggregate, CancellationToken token = default);

        /// <summary>
        /// Store multiple aggregate roots to the storage backend.
        /// </summary>
        /// <param name="aggregates">The aggregate roots to be stored.</param>
        /// <param name="token">Cancellation Token if the operation needs to be cancelled.</param>
         Task SaveAllAsync(IEnumerable<TAggregateRoot> aggregates, CancellationToken token = default);
    }
}