namespace Code.OrmFramework.MultiTenancy
{
    public class TenantProvider : ITenantProvider
    {
        private readonly Dictionary<string, string> _tenants;
        private string _currentTenant = "DefaultTenant";

        public TenantProvider(Dictionary<string, string> tenantConnections)
        {
            _tenants = tenantConnections;
        }

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
            return _tenants.TryGetValue(tenantId, out var connStr) ? connStr : null;
        }
    }
}
