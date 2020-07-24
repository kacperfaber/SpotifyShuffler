using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyUri
    {
        [JsonProperty("uri")]
        public string ItemUri { get; set; }
    }
}