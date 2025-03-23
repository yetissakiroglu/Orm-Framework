namespace Code.OrmFramework.MultiTenancy
{
    public interface ITenantProvider
    {
        string GetCurrentTenant();  // Mevcut kiracıyı döndürür
        void SetCurrentTenant(string tenantId);  // Kiracıyı ayarlar
        string GetConnectionString(string tenantId);  // Kiracının veritabanı bağlantısını döndürür
    }
}
