using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyTrack
    {
        [JsonProperty("album")]
        public SpotifyAlbum Album { get; set; }

        [JsonProperty("artists")]
        public SpotifyArtist[] Artists { get; set; }
        
        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }
        
        [JsonProperty("external_urls")]
        public SpotifyExternalUrls ExternalUrls { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}