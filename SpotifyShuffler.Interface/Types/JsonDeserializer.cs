using Newtonsoft.Json;
using SpotifyShuffler.Interface.Interfaces;

namespace SpotifyShuffler.Interface.Types
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}