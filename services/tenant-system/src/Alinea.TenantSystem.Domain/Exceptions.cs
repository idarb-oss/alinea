namespace Alinea.TenantSystem.Domain.Tenant
{
    [System.Serializable]
    public class TenantException : System.Exception
    {
        public TenantException() { }
        public TenantException(string message) : base(message) { }
        public TenantException(string message, System.Exception inner) : base(message, inner) { }
        protected TenantException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class GroupException : System.Exception
    {
        public GroupException() { }
        public GroupException(string message) : base(message) { }
        public GroupException(string message, System.Exception inner) : base(message, inner) { }
        protected GroupException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class UserException : System.Exception
    {
        public UserException() { }
        public UserException(string message) : base(message) { }
        public UserException(string message, System.Exception inner) : base(message, inner) { }
        protected UserException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}