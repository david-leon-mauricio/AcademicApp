using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AcademicApp.Storage
{
    public class LocalMemory : ICache
    {
        private IMemoryCache _cache;

        public LocalMemory(MemoryCacheOptions options)
        {
            _cache = new MemoryCache(options);
        }

        public bool Contains(string key)
        {
            return _cache.TryGetValue(key, out object value);
        }

        public bool Add(string key, object value)
        {
            lock (_cache)
            {
                if (Contains(key))
                    return false;
                else
                {
                    Set(key, value);
                    return true;
                }
            }
        }

        public void Set(string key, object value)
        {
            lock (_cache)
            {
                _cache.Set(key, value);
            }
        }

        public T Get<T>(string key)
        {
            lock (_cache)
            {
                if (_cache.Get(key) != null)
                {
                    return (T)_cache.Get(key);
                }
            }

            return default;
        }

        public bool Remove(string key)
        {
            bool wasRemoved;
            lock (_cache)
            {
                wasRemoved = Contains(key);
                _cache.Remove(key);
            }
            return wasRemoved;
        }
    }
}
