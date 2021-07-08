using System.Collections.Generic;

namespace Alinea.Core.Abstraction.EventSourcing
{
    /// <summary>
    /// Initialize an aggregate.
    /// </summary>
    public interface IAggregateInitializer
    {
        /// <summary>
        /// Initialize this instance using the specified events.
        /// </summary>
        /// <param name="events">The events to initialize with.</param>
         void Initialize(IEnumerable<IEvent> events);
    }
}