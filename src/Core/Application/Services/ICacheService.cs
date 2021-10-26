using System;

namespace Application.Services
{
    public interface ICacheService
    {
        void Set(string key, object value, TimeSpan? slidingExpiration);
        object Get(string key);
    }
}