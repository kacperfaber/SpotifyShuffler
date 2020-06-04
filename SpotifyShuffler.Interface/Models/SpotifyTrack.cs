using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyTrack
    {
        [JsonProperty("album")]
        public SpotifySimpleAlbum Album { get; set; }
        
        [JsonProperty("artists")]
        public SimpleSpotifyArtist[] Artists { get; set; }
        
        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public SpotifyType Type { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}