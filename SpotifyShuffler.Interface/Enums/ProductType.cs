using Newtonsoft.Json;

namespace SpotifyShuffler.Interface.Enums
{
    public enum ProductType
    {
        [JsonProperty("free")]
        Free,
        
        [JsonProperty("premium")]
        Premium
    }
}