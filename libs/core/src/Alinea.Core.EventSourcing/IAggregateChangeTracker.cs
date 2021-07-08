using System.Collections.Generic;

namespace Alinea.Core.Abstraction.EventSourcing
{
    /// <summary>
    /// Tracks changes that happens to an aggregate.
    /// </summary>
    public interface IAggregateChangeTracker
    {
        /// <summary>
        /// Determines wheater this instance has state changes.
        /// </summary>
        /// <returns> <c>true</c> if this instance has state changes, otherwise <c>false</c>. </returns>
        bool HasChange();

        /// <summary>
        /// Gets the state changes applied to this instance.
        /// </summary>
        /// <returns> A list of recoreded state changes.</returns>
        IEnumerable<IEvent> GetChanges();

        /// <summary>
        /// Clear the state changes from the instance.
        /// </summary>
        void ClearChanges();
    }
}