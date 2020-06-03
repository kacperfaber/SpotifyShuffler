using Newtonsoft.Json;

namespace SpotifyShuffler.Api
{
    public class SpotifyUser
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
        
        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}