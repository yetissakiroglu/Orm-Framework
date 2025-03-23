namespace Code.OrmFramework.Caching
{
    public interface ICacheService
    {
        void Set(string key, object value);
        object Get(string key);
        void Remove(string key);
    }
}
