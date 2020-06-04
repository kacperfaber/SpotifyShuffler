using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public enum AlbumGroupType
    {
        [JsonProperty("album")]
        Album,

        [JsonProperty("single")]
        Single,

        [JsonProperty("compilation")]
        Compilation,

        [JsonProperty("appears_on")]
        AppearsOn
    }
}