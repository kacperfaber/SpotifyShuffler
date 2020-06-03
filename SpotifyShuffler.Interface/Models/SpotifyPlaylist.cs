using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyPlaylist
    {
        [JsonProperty("collaborative")]
        public bool Collaborative { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("followers")]
        public SpotifyFollowers Followers { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("owner")]
        public PublicSpotifyUser Owner { get; set; }
        
        [JsonProperty("public")]
        public bool IsPublic { get; set; }
        
        [JsonProperty("tracks")]
        public Paging<PlaylistTrackObject> Tracks { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}