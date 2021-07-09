using System;
using System.Linq;
using System.Collections.Generic;

namespace Alinea.Core.EventSourcing
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        readonly IAggregateChangeTracker _changeTracker;
        readonly IEventRouter _eventRouter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        protected AggregateRoot() : this(new ChangeTracker(), new EventRouter()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootEntity"/> class.
        /// </summary>
        /// <param name="changeTracker">The aggregate change tracker.</param>
        /// <param name="router">The event router.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="router"/> is <c>null</c></exception>
        protected AggregateRoot(IAggregateChangeTracker changeTracker, IEventRouter router)
        {
            if (router == null) throw new ArgumentNullException(nameof(router));

            _changeTracker = changeTracker;
            _eventRouter = router;
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

            _eventRouter.ConfigureRoute(handler);
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

            foreach (var @event in events)
                ApplyChange(@event);
        }

        /// <inheritdoc/>
        public bool HasChange()
        {
            return _changeTracker.Any();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetChanges()
        {
            return _changeTracker.ToList();
        }

        /// <inheritdoc/>
        public void ClearChanges()
        {
            _changeTracker.Reset();
        }

        /// <summary>
        /// Apply changes to the aggregate root
        /// </summary>
        public void ApplyChange<TEvent>(TEvent @event) where TEvent : IEvent
        {
            ApplyChange(@event, true);
        }

        /// <summary>
        /// Apply changes to the aggregate root.
        /// If it is a new event we want to record it to the event storage. Else we just handle it,
        /// to get the state of the aggregate root.
        /// </summary>
        /// <param name="events">The event to apply changes from.</param>
        /// <param name="newEvent">If it is a new Event that should be tracked.</param>
        protected void ApplyChange(IEvent @event, bool newEvent = false)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            Route(@event);

            if (newEvent)
                TrackChange(@event);
        }

        void Route(IEvent @event)
        {
            _eventRouter.Route(@event);
        }

        void TrackChange(IEvent @event)
        {
            _changeTracker.TrackChange(@event);
        }
    }
}