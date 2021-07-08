using System;

namespace Alinea.Core.Abstraction.EventSourcing
{
    /// <summary>
    /// Aggregate root marker interface
    /// </summary>
    public interface IAggregateRoot : IAggregateInitializer, IAggregateChangeTracker
    {
    }
}