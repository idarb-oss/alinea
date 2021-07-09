using System.Collections.Generic;

using Alinea.TenantSystem.Domain.Group;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public interface ITenantFactory
    {
         Tenant Create(TenantDomain domain, string description, IEnumerable<GroupId> groups);
    }
}