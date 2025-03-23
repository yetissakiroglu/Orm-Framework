namespace Code.OrmFramework.MultiTenancy
{
    public class TenantProvider(Dictionary<string, string> tenantConnections) : ITenantProvider
    {
        private readonly Dictionary<string, string> _tenants = tenantConnections;
        private string _currentTenant;

        public string GetCurrentTenant() => _currentTenant;

        public void SetCurrentTenant(string tenantId)
        {
            if (_tenants.ContainsKey(tenantId))
            {
                _currentTenant = tenantId;
            }
        }

        public string GetConnectionString(string tenantId)
        {
            return _tenants.TryGetValue(tenantId, out var connectionString) ? connectionString : null;
        }
    }
}
