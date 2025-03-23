namespace Code.OrmFramework.MultiTenancy
{
    public interface ITenantProvider
    {
        string GetCurrentTenant();
        void SetCurrentTenant(string tenantId);
        string GetConnectionString(string tenantId);
    }
}
