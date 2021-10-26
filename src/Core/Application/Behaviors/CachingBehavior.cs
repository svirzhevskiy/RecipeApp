using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Common;
using Application.Services;
using Application.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableMediatrQuery
    {
        private readonly ICacheService _cache;
        private readonly CacheSettings _settings;

        public CachingBehavior(ICacheService cache, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;

            if (request.BypassCache)
            {
                return await next();
            }

            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();

                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromHours(_settings.SlidingExpiration);

                _cache.Set(request.CacheKey, response, slidingExpiration);

                return response;
            }

            var cachedResponse = _cache.Get(request.CacheKey);

            if (cachedResponse is not null)
            {
                response = (TResponse)cachedResponse;
            }
            else
            {
                response = await GetResponseAndAddToCache();
            }

            return response;
        }
    }
}