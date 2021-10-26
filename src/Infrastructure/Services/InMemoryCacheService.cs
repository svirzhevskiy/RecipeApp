using System;
using Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Services
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set(string key, object value, TimeSpan? slidingExpiration)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions { SlidingExpiration = slidingExpiration });
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }
    }
}