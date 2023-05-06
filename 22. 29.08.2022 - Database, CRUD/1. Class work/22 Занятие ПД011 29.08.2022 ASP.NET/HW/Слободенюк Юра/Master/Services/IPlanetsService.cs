using Master.Common;
using Master.Models;
using Microsoft.Extensions.Caching.Memory;


namespace Master.Services;


public interface IPlanetsService
{
    Task<Paginated<Planet>> GetPlanetsAsync(int page);
    Task<Planet> GetPlanetAsync(int id);
}


public sealed class PlanetsService : IPlanetsService
{
    public const string Route = "planets";
    private readonly IMemoryCache _cache;
    private readonly IHttpClientFactory _httpClientFactory;


    public PlanetsService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }


    public async Task<Paginated<Planet>> GetPlanetsAsync(int page)
    {
        var pageData = await LockedCache<Paginated<Planet>>.GetOrCreateAsync(
            _cache,
            $"P-pl-{page}",
            async () =>
            {
                using var client = _httpClientFactory.CreateStarWarsClient();
                var request = new Uri($"{Route}?page={page}", UriKind.Relative);
                var response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var pageData = await response.Content.ReadFromJsonAsync<Paginated<Planet>>(SharedOptions.JsonSerializerOptions);

                return pageData!;
            },
            new() { SlidingExpiration = TimeSpan.FromMinutes(1), Priority = CacheItemPriority.Low });

        return pageData;
    }


    public async Task<Planet> GetPlanetAsync(int id)
    {
        var result = await LockedCache<Planet>.GetOrCreateAsync(
            _cache,
            $"pl-{id}",
            async () =>
            {
                using var client = _httpClientFactory.CreateStarWarsClient();
                var request = new Uri($"{Route}/{id}", UriKind.Relative);
                var response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var planet = await response.Content.ReadFromJsonAsync<Planet>(SharedOptions.JsonSerializerOptions);

                return planet!;
            },
            new() { SlidingExpiration = TimeSpan.FromMinutes(3) });

        return result;
    }
}