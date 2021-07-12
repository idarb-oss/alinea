using System;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public record TenantId(Guid Id);

    public record TenantDomain(string Domain);

    public record TenantDescription(string Description);
}