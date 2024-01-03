namespace Api.Infrastructure.Cache;

public interface ICacheableMediatrQuery
{
    bool BypassCache { get; init; }
    string CacheKey { get; }
}
