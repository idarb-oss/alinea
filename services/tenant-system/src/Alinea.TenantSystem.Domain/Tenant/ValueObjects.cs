using System;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public record TenantId(Guid id);

    public record TenantDomain(string domain);
}