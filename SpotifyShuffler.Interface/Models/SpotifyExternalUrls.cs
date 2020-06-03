using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}