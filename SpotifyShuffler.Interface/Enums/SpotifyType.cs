using Newtonsoft.Json;

namespace SpotifyShuffler.Interface.Enums
{
    public enum SpotifyType
    {
        [JsonProperty("artist")]
        Artist,

        [JsonProperty("album")]
        Album,

        [JsonProperty("playlist")]
        Playlist,

        [JsonProperty("track")]
        Track,

        [JsonProperty("episode")]
        Episode,

        [JsonProperty("show")]
        Show,

        [JsonProperty("user")]
        User
    }
}