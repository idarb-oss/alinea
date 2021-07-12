using System;
using Alinea.Core.EventSourcing;

namespace Alinea.TenantSystem.Domain
{
    public class BaseAggregate : AggregateRoot
    {
        public DateTimeOffset CreateTime { get; protected set; }

        public DateTimeOffset UpdateTime { get; protected set; }

        public DateTimeOffset DeleteTime { get; protected set; }
    }
}