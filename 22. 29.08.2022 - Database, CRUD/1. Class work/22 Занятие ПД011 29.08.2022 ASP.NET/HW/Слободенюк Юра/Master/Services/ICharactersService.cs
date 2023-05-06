using System.Text.Json;
using System.Text.Json.Serialization;
using Master.Common;
using Master.Models;
using Microsoft.Extensions.Caching.Memory;


namespace Master.Services;


public interface ICharactersService
{
    Task<Paginated<Character>> GetCharactersAsync(int page);

    Task<Character> GetCharacterAsync(int id);
}


public sealed class CharactersService : ICharactersService
{
    public const string Route = "people";

    private readonly IMemoryCache _cache;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IPlanetsService _planetsService;


    public CharactersService(IHttpClientFactory httpClientFactory, IMemoryCache cache, IPlanetsService planetsService)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _planetsService = planetsService;
    }


    public async Task<Paginated<Character>> GetCharactersAsync(int page)
    {
        var pageData = await LockedCache<Paginated<Character>>.GetOrCreateAsync(
            _cache,
            $"P-ch-{page}",
            async () =>
            {
                using var client = _httpClientFactory.CreateStarWarsClient();
                var request = new Uri($"{Route}?page={page}", UriKind.Relative);
                var response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var pageData = await response.Content.ReadFromJsonAsync<Paginated<Character>>(SharedOptions.JsonSerializerOptions);

                var homeworldsTasks = pageData!.Results!.Select(ConnectCharacterWithHomeworld);

                await Task.WhenAll(homeworldsTasks);

                return pageData;
            },
            new() { SlidingExpiration = TimeSpan.FromMinutes(1), Priority = CacheItemPriority.Low });

        return pageData;
    }


    public async Task<Character> GetCharacterAsync(int id)
    {
        var result = await LockedCache<Character>.GetOrCreateAsync(
            _cache,
            $"ch-{id}",
            async () =>
            {
                using var client = _httpClientFactory.CreateStarWarsClient();
                var request = new Uri($"{Route}/{id}", UriKind.Relative);
                var response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();

                var character = await response.Content.ReadFromJsonAsync<Character>(SharedOptions.JsonSerializerOptions);
                await ConnectCharacterWithHomeworld(character!);

                return character!;
            },
            new() { SlidingExpiration = TimeSpan.FromMinutes(3) });

        return result;
    }


    private Task ConnectCharacterWithHomeworld(Character character)
    {
        var planetUrl = new Uri(character.Homeworld!);
        var id = planetUrl.ExtractSegment(^1);

        return _planetsService.GetPlanetAsync(int.Parse(id))
            .ContinueWith(planet => { character.Homeworld = planet.Result.Name; });
    }
}