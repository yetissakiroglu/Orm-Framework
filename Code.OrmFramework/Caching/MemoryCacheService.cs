namespace Code.OrmFramework.Caching
{

    public class MemoryCacheService : ICacheService
    {
        private readonly Dictionary<string, object> _cache = new();

        public void Set(string key, object value)
        {
            _cache[key] = value;
        }

        public object Get(string key)
        {
            _cache.TryGetValue(key, out var value);
            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
