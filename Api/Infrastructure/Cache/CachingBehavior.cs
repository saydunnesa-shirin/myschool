using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;

namespace Api.Infrastructure.Cache;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : notnull
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;
    public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is ICacheableMediatrQuery cacheableQuery && !cacheableQuery.BypassCache)
        {
            return await GetResponseFromCacheOrExecute(cacheableQuery, next, cancellationToken);
        }
        return await next();
    }

    private async Task<TResponse> GetResponseFromCacheOrExecute(ICacheableMediatrQuery cacheableQuery, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var cachedResponse = await _cache.GetAsync(cacheableQuery.CacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                _logger.LogDebug("Fetched from cache -> {cacheableQuery.CacheKey}", cacheableQuery.CacheKey);
                return JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse))!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving data from cache for key -> {cacheableQuery.CacheKey}", cacheableQuery.CacheKey);
        }

        return await GetResponseAndAddToCache(cacheableQuery, next, cancellationToken);
    }

    private async Task<TResponse> GetResponseAndAddToCache(ICacheableMediatrQuery cacheableQuery, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) };
        var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));

        try
        {
            await _cache.SetAsync(cacheableQuery.CacheKey, serializedData, options, cancellationToken);
            _logger.LogDebug("Added to cache -> {cacheableQuery.CacheKey}", cacheableQuery.CacheKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing data in cache for key -> {cacheableQuery.CacheKey}", cacheableQuery.CacheKey);
        }

        return response;
    }

}
