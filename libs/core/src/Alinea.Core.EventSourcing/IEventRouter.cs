using System;

namespace Alinea.Core.EventSourcing
{
    /// <summary>
    /// Routes an event to a configured state handler
    /// </summary>
    public interface IEventRouter
    {
        /// <summary>
        /// Routes the specified <paramref name="event"/> to a configured state handler, if any.
        /// </summary>
        /// <param name="event">The event to route.</param>
        /// <exception cref="ArgumentNullException">Thown when the <paramref name="event"/> is null.</exception>
         void Route(IEvent @event);

        /// <summary>
        /// Adds a route for the specified event type to the specified state handler.
        /// </summary>
        /// <param name="event">The event type to handle.</param>
        /// <param name="handler">The state handler that should be invoked when an event of the specified type is routed.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="event"/> is null.</exception>
         void ConfigureRoute(Type @event, Action<IEvent> handler);

        /// <summary>
        /// Adds a route for the specified event type to the specified state handler
        /// </summary>
        /// <param name="handler">The state handler that should be invoked when an event of the specified type is routed.</param>
        /// <typeparam name="TEvent">The event type the route is for.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="event"/> is null.</exception>
         void ConfigureRoute<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    }
}