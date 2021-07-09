using System.Collections.Generic;
using Alinea.Core.EventSourcing;
using Alinea.TenantSystem.Domain.User;

namespace Alinea.TenantSystem.Domain.Group
{
    public class Group : AggregateRoot
    {
        private List<UserId> _owners;
        private List<GroupId> _groups;

        public GroupId GroupId { get; private set; }

        public string ExternalId { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<UserId> Owners { get { return _owners; } }

        public IEnumerable<GroupId> Groups { get { return _groups; } }
    }
}