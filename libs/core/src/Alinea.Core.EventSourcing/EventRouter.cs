using System;
using System.Collections.Generic;

namespace Alinea.Core.Abstraction.EventSourcing
{
    public class EventRouter
    {
        readonly Dictionary<Type, Action<IEvent>> _handlers;

        public EventRouter()
        {
            _handlers = new Dictionary<Type, Action<IEvent>>();
        }

        public void AddRoute(Type @event, Action<IEvent> handler)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            _handlers.Add(@event, handler);
        }

        public void AddRoute<TEvent>(Action<TEvent> handler) where TEvent : IEvent
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