namespace Alinea.TenantSystem.Domain.Group
{
    public interface IGroupFactory
    {
         Group Create(string externalId, string description);
    }
}