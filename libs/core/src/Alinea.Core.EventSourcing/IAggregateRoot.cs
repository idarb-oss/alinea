using System;
using System.Collections.Generic;

namespace Alinea.Core.EventSourcing
{
    /// <summary>
    /// Aggregate root marker interface
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Send a new command to the aggregate to be handled.
        /// </summary>
        /// <param name="command">The command to be handled by the aggregate.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="command"/> are null.</exception>
        void Handle(ICommand command);

        /// <summary>
        /// Initialize this instance using the specified events.
        /// </summary>
        /// <param name="events">The events to initialize with.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="events"/> are null.</exception>
         void Initialize(IEnumerable<IEvent> events);

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