using System;
using System.Collections.Generic;
using Alinea.Core.EventSourcing;
using Alinea.TenantSystem.Domain.Group;
using Alinea.TenantSystem.Domain.User;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public record CreateTenantEvent(
        TenantId TenantId,
        TenantDomain Domain,
        TenantDescription Description,
        UserId Owner,
        GroupId AdminGroup,
        DateTimeOffset CreateTime
    ) : IEvent {}

    public record UpdateTenantEvent(
        TenantId TenantId,
        TenantDomain Domain,
        TenantDescription Description,
        DateTimeOffset UpdateTime
    ) : IEvent {}

    public record DeleteTenantEvent(TenantId TenantId, DateTimeOffset DeleteTime) : IEvent {}

    public record AddGroupsEvent(TenantId TenantId, IEnumerable<GroupId> Groups, DateTimeOffset UpdateTime) : IEvent {}

    public record RemoveGroupsEvent(TenantId TenantId, IEnumerable<GroupId> Groups, DateTimeOffset UpdateTime) : IEvent {}

    public record AddOwnersEvent(TenantId TenantId, IEnumerable<UserId> Owners, DateTimeOffset UpdateTime) : IEvent {}

    public record RemoveOwnersEvent(TenantId TenantId, IEnumerable<UserId> Owners, DateTimeOffset UpdateTime) : IEvent {}
}