using Newtonsoft.Json;

namespace SpotifyShuffler.Interface.Enums
{
    public enum AlbumType
    {
        [JsonProperty("album")]
        Album,
        
        [JsonProperty("single")]
        Single,
        
        [JsonProperty("compilation")]
        Compilation
    }
}