namespace Alinea.Core.Abstraction.EventSourcing
{
    /// <summary>
    /// Routes an event to a configured state handler
    /// </summary>
    public interface IEventRouter
    {
        /// <summary>
        /// Routes the specified <paramref name="evebt"/> to a configured state handler, if any.
        /// </summary>
        /// <param name="event">The event to route.</param>
        /// <exception cref="ArgumentNullException">Thown when the <paramref name="event"/> is null.</exception>
         void Route(object @event);
    }
}