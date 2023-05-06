using System.Text.Json;
using System.Text.Json.Serialization;


namespace Master.Common;


public static class SharedOptions
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
}