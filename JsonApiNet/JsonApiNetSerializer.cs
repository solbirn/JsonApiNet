using JsonApiNet.Components;
using JsonApiNet.Helpers;
using JsonApiNet.JsonConverters;
using Newtonsoft.Json;

namespace JsonApiNet
{
    public class JsonApiNetSerializer : IJsonApiNetSerializer
    {
        public JsonApiSettings Settings { get; set; }

        public JsonApiNetSerializer(JsonApiSettings settings)
        {
            Settings = settings;
        }

        public dynamic ResourceFromDocument(string json)
        {
            var document = Document(json);
            return document.Resource;
        }

        public string DocumentFromResource(dynamic json)
        {
            var document = JsonDocument(json);
            return document;
        }

        public JsonApiDocument Document(string json)
        {
            return JsonConvert.DeserializeObject<JsonApiDocument>(
                json, 
                new JsonSerializerSettings
                    {
                        ContractResolver = new ContractResolver(Settings)
                    });
        }

        public string JsonDocument(dynamic json)
        {
            return JsonConvert.SerializeObject(
                json,
                new JsonSerializerSettings
                {
                    ContractResolver = new ContractResolver(Settings),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}