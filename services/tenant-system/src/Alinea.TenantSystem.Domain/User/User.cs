using System.Collections.Generic;
using Alinea.Core.EventSourcing;
using Alinea.TenantSystem.Domain.Group;

namespace Alinea.TenantSystem.Domain.User
{
    public class User : AggregateRoot
    {
        private List<GroupId> _groups;
        
        public UserId UserId { get; private set; }

        public string ExternalId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public IEnumerable<GroupId> Groups { get { return _groups; } }
    }
}