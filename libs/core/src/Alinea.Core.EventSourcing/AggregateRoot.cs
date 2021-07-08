using System;
using System.Linq;
using System.Collections.Generic;

namespace Alinea.Core.Abstraction.EventSourcing
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        readonly ChangeTracker _changeTracker;
        readonly EventRouter _eventRouter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        protected AggregateRoot()
        {
            _changeTracker = new ChangeTracker();
            _eventRouter = new EventRouter();
        }

        /// <summary>
        /// Register a state handler to be invoked when the specified event is applied.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <typeparam name="TEvent">The type of the event to register the handler for.</typeparam>
        /// <exception cref="ArgumentNullException">Throw when the <paramref name="handler"/> are null.</exception>
        protected void Register<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _eventRouter.AddRoute(handler);
        }

        /// <summary>
        /// Initializes this instance using the specified events.
        /// </summary>
        /// <param name="events">The events to initialize with.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="events"/> are null.</exception>
        public void Initialize(IEnumerable<IEvent> events)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));
            if (HasChange())
                throw new InvalidOperationException("Initialize cannot be called on an instance with changes.");
            foreach(var @event in events)
                Play(@event);
        }

        protected void ApplyChange(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
        }

        protected virtual void BeforeApplyChange(IEvent @event) {}

        protected virtual void AfterApplyChangfe(IEvent @event) {}

        void Play(IEvent @event)
        {
            _eventRouter.Route(@event);
        }

        void Record(IEvent @event)
        {
            _changeTracker.Record(@event);
        }

        public bool HasChange()
        {
            return _changeTracker.Any();
        }

        public IEnumerable<IEvent> GetChanges()
        {
            return _changeTracker.ToArray();
        }

        public void ClearChanges()
        {
            _changeTracker.Reset();
        }
    }
}