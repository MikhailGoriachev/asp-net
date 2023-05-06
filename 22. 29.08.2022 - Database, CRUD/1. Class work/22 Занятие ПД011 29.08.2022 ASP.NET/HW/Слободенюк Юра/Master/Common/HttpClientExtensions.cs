using System.Text.Json;


namespace Master.Common;


public static class HttpClientExtensions
{
    public const string StarWarsClient = "characters";


    public static HttpClient CreateStarWarsClient(this IHttpClientFactory factory) =>
        factory.CreateClient(StarWarsClient);
}