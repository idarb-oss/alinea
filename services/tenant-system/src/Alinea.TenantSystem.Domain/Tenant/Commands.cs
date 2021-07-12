using System.Collections.Generic;
using Alinea.Core.EventSourcing;
using Alinea.TenantSystem.Domain.Group;
using Alinea.TenantSystem.Domain.User;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public record CreateTenantCommand(
        TenantDomain Domain,
        TenantDescription Description,
        UserId Owner,
        GroupId AdminGroup
    ) : ICommand {}

    public record UpdateTenantCommand(
        TenantId TenantId,
        TenantDomain Domain,
        TenantDescription Description
    ) : ICommand {}

    public record DeleteTenantCommand(TenantId TenantId) : ICommand {}

    public record AddGroupsCommand(TenantId TenantId, IEnumerable<GroupId> Groups) : ICommand {}

    public record RemoveGroupsCommand(TenantId TenantId, IEnumerable<GroupId> Groups) : ICommand {}

    public record AddOwnersCommand(TenantId TenantId, IEnumerable<UserId> Owners) : ICommand {}

    public record RemoveOwnersCommand(TenantId TenantId, IEnumerable<UserId> Owners) : ICommand {}
}