using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public enum ProductType
    {
        [JsonProperty("free")]
        Free,

        [JsonProperty("premium")]
        Premium
    }
}