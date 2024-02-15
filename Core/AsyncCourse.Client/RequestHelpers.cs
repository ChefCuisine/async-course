using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace AsyncCourse.Client;

public static class RequestHelpers
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = false
            }
        },
        Converters = {new StringEnumConverter(new CamelCaseNamingStrategy())}
    };

    public static string Serialize<T>([NotNull] T data, JsonSerializerSettings serializerSettings = null)
    {
        return JsonConvert.SerializeObject(data, serializerSettings ?? jsonSerializerSettings);
    }

    public static T Deserialize<T>([NotNull] string data, JsonSerializerSettings serializerSettings = null)
    {
        return JsonConvert.DeserializeObject<T>(data, serializerSettings ?? jsonSerializerSettings);
    }
    
    public static Request WithJsonContent<T>(
        this Request request,
        T? content,
        JsonSerializerSettings serializerSettings = null)
    {
        if (ReferenceEquals(content, null))
        {
            return request.WithContentTypeHeader("application/json");
        }

        return request
            .WithContentTypeHeader("application/json")
            .WithContent(Serialize(content, serializerSettings));
    }
}