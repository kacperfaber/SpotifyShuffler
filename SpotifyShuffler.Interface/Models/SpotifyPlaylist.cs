using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyPlaylist
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public SpotifyPlaylistItem Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        // TODO what is it, what is him type
        [JsonProperty("next")]
        public int Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        // TODO what is it, what is him type
        [JsonProperty("previous")]
        public int Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}