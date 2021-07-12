using System;
using System.Collections.Generic;

using Alinea.TenantSystem.Domain.Group;
using Alinea.TenantSystem.Domain.User;

namespace Alinea.TenantSystem.Domain.Tenant
{
    public sealed class Tenant : BaseAggregate
    {
        readonly HashSet<GroupId> _groups;

        readonly HashSet<UserId> _owners;

        public Tenant() : base()
        {
            // Command Handlers
            RegisterCommand<CreateTenantCommand>(HandleCreateTennant);
            RegisterCommand<UpdateTenantCommand>(HandleUpdateTenant);
            RegisterCommand<DeleteTenantCommand>(HandleDeleteTenant);
            RegisterCommand<AddGroupsCommand>(HandleAddGroups);
            RegisterCommand<RemoveGroupsCommand>(HandleRemoveGroups);
            RegisterCommand<AddOwnersCommand>(HandleAddOwners);
            RegisterCommand<RemoveOwnersCommand>(HandleRemoveOwners);

            // Events
            RegisterEvent<CreateTenantEvent>(OnCreateTenant);
            RegisterEvent<UpdateTenantEvent>(OnUpdateTenant);
            RegisterEvent<DeleteTenantEvent>(OnDeleteTenant);
            RegisterEvent<AddGroupsEvent>(OnAddGroups);
            RegisterEvent<RemoveGroupsEvent>(OnRemoveGroups);
            RegisterEvent<AddOwnersEvent>(OnAddOwners);
            RegisterEvent<RemoveOwnersEvent>(OnRemoveOwners);
        }

        #region Properties

        public TenantId TenantId { get; private set; }

        public TenantDomain Domain { get; private set; }

        public TenantDescription Description { get; private set; }

        public IEnumerable<UserId> Owners { get { return _owners; } }

        public IEnumerable<GroupId> Groups { get { return _groups; } }

        #endregion

        #region Command Handlers

        void HandleCreateTennant(CreateTenantCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Domain.Domain))
                throw new TenantException(nameof(command.Domain));

            if (command.AdminGroup.Id == Guid.Empty)
                throw new TenantException(nameof(command.AdminGroup));

            if (command.Owner.Id == Guid.Empty)
                throw new TenantException(nameof(command.Owner));

            var id = new TenantId(Guid.NewGuid());

            ApplyChange(
                new CreateTenantEvent(
                    id,
                    command.Domain,
                    command.Description,
                    command.Owner,
                    command.AdminGroup,
                    DateTimeOffset.UtcNow
                )
            );
        }

        void HandleUpdateTenant(UpdateTenantCommand command)
        {
            if (TenantId != command.TenantId)
                throw new TenantException(nameof(command.TenantId));
            
            var domain = command.Domain;
            if (string.IsNullOrWhiteSpace(domain.Domain))
                domain = Domain;

            var description = command.Description;
            if (string.IsNullOrWhiteSpace(description.Description))
                description = Description;

            ApplyChange(
                new UpdateTenantEvent(
                    TenantId,
                    domain,
                    description,
                    DateTimeOffset.UtcNow
                )
            );
        }

        void HandleDeleteTenant(DeleteTenantCommand command)
        {
            if (TenantId != command.TenantId)
                throw new TenantException(nameof(command.TenantId));

            ApplyChange(
                new DeleteTenantEvent(
                    command.TenantId,
                    DateTimeOffset.UtcNow
                )
            );
        }

        void HandleAddGroups(AddGroupsCommand command)
        {
            if (TenantId != command.TenantId)
                throw new TenantException(nameof(command.TenantId));

            ApplyChange(
                new AddGroupsEvent(
                    command.TenantId,
                    command.Groups,
                    DateTimeOffset.UtcNow
                )
            );
        }

        void HandleRemoveGroups(RemoveGroupsCommand command)
        {
            if (TenantId != command.TenantId)
                throw new TenantException(nameof(command.TenantId));

            ApplyChange(
                new RemoveGroupsEvent(
                    command.TenantId,
                    command.Groups,
                    DateTimeOffset.UtcNow
                )
            );
        }

        void HandleAddOwners(AddOwnersCommand command)
        {
            if (TenantId != command.TenantId)
                throw new TenantException(nameof(command.TenantId));

            ApplyChange(
                new AddOwnersEvent(
                    command.TenantId,
                    command.Owners,
                    DateTimeOffset.UtcNow
                )
            );
        }

        void HandleRemoveOwners(RemoveOwnersCommand command)
        {
            if (TenantId != command.TenantId)
                throw new TenantException(nameof(command.TenantId));

            ApplyChange(
                new RemoveOwnersEvent(
                    command.TenantId,
                    command.Owners,
                    DateTimeOffset.UtcNow
                )
            );
        }

        #endregion

        #region On Events

        void OnCreateTenant(CreateTenantEvent @event)
        {
            TenantId = @event.TenantId;
            Domain = @event.Domain;
            Description = @event.Description;
            _owners.Add(@event.Owner);
            _groups.Add(@event.AdminGroup);
            CreateTime = @event.CreateTime;
        }

        void OnUpdateTenant(UpdateTenantEvent @event)
        {
            Domain = @event.Domain;
            Description = @event.Description;
            UpdateTime = @event.UpdateTime;
        }

        void OnDeleteTenant(DeleteTenantEvent @event)
        {
            DeleteTime = @event.DeleteTime;
            UpdateTime = @event.DeleteTime;
        }

        void OnAddGroups(AddGroupsEvent @event)
        {
            _groups.UnionWith(@event.Groups);
            UpdateTime = @event.UpdateTime;
        }

        void OnRemoveGroups(RemoveGroupsEvent @event)
        {
            _groups.ExceptWith(@event.Groups);
            UpdateTime = @event.UpdateTime;
        }

        void OnAddOwners(AddOwnersEvent @event)
        {
            _owners.UnionWith(@event.Owners);
            UpdateTime = @event.UpdateTime;
        }

        void OnRemoveOwners(RemoveOwnersEvent @event)
        {
            _owners.ExceptWith(@event.Owners);
            UpdateTime = @event.UpdateTime;
        }

        #endregion
    }
}