using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyArtist
    {
        [JsonProperty("external_urls")]
        public SpotifyExternalUrls ExternalUrls { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}