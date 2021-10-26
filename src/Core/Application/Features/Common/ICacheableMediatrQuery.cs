using System;

namespace Application.Features.Common
{
    
    public interface ICacheableMediatrQuery
    {
        /// <summary>
        /// Determines if you want to skip caching and go directly to the datastore
        /// </summary>
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}