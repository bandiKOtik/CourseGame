using Newtonsoft.Json;

namespace Assets.Scripts.Utilities.DataManagement.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        private JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.Auto,
        };

        public TData Deserialize<TData>(string serializedData)
            => JsonConvert.DeserializeObject<TData>(serializedData, settings);

        public string Serialize<TData>(TData data)
            => JsonConvert.SerializeObject(data, settings);
    }
}