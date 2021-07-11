using System;
using System.Collections.Generic;

namespace Alinea.Core.EventSourcing
{
    public class EventRouter : IEventRouter
    {
        readonly Dictionary<Type, Action<IEvent>> _handlers;

        public EventRouter()
        {
            _handlers = new();
        }

        public void ConfigureRoute(Type @event, Action<IEvent> handler)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(@event, handler);
        }

        public void ConfigureRoute<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(typeof(TEvent), @event => handler((TEvent) @event));
        }

        public void Route(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            Action<IEvent> handler;
            if (_handlers.TryGetValue(@event.GetType(), out handler))
            {
                handler(@event);
            }
        }
    }
}