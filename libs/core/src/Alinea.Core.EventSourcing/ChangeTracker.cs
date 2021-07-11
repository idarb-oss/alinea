using System;
using System.Collections;
using System.Collections.Generic;

namespace Alinea.Core.EventSourcing
{
    /// <summary>
    /// Records events applied to an aggregate's root.
    /// </summary>
    public class ChangeTracker : IChangeTracker
    {
        readonly List<IEvent> _changes;

        /// <summary>
        /// Initialize a new instance of the <see cref="ChangeTracker"/> class.
        /// </summary>
        public ChangeTracker()
        {
            _changes = new List<IEvent>();
        }

        /// <summary>
        /// Records that the specified event happend.
        /// </summary>
        /// <param name="event">The event to record.</param>
        /// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="event"/> is <c>null</c>.</exception>
        public void TrackChange(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            _changes.Add(@event);
        }


        /// <summary>
        /// Resets this instnce to its initial state.
        /// </summary>
        public void Reset()
        {
            _changes.Clear();
        }

        /// <summary>
        /// Gets an enumeration of recorded events.
        /// </summary>
        /// <returns>The recorded event enumerator.</returns>
        public IEnumerator<IEvent> GetEnumerator()
        {
            return _changes.GetEnumerator();
        }

        /// <summary>
        /// Returns en enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}