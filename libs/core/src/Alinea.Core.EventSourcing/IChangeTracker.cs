using System.Collections.Generic;

namespace Alinea.Core.EventSourcing
{
    /// <summary>
    /// Tracks changes that happens to an aggregate.
    /// </summary>
    public interface IChangeTracker : IEnumerable<IEvent>
    {
        /// <summary>
        /// Tracks the changes to an aggregate root.
        /// </summary>
        void TrackChange(IEvent @event);

        /// <summary>
        /// Reset the changes to the aggregate root.
        /// </summary>
        void Reset();
    }
}