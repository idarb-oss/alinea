using System.Collections.Generic;

using Alinea.Core.EventSourcing;
using Alinea.TenantSystem.Domain.Group;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public class Tenant : AggregateRoot
    {
        private List<GroupId> _groups;
        
         public TenantId TenantId { get; private set; }

         public TenantDomain Domain { get; private set; }

         public string Description { get; private set; }

         IEnumerable<GroupId> Groups { get { return _groups; } }
    }
}