using Microsoft.Extensions.Caching.Memory;


namespace Master.Common;


public static class LockedCache<T>
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);


    public static async Task<T> GetOrCreateAsync(
        IMemoryCache cache,
        string key,
        Func<Task<T>> factory,
        MemoryCacheEntryOptions? cacheOptions = default
    )
    {
        var cached = cache.Get<T>(key);

        if (cached is not null)
            return cached;
        
        await Semaphore.WaitAsync();
        try
        {
            cached = cache.Get<T>(key);

            if (cached is not null)
                return cached;

            cached = await factory.Invoke();

            cache.Set(key, cached, cacheOptions);
            return cached;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}