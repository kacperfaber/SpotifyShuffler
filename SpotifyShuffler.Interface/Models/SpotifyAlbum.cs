using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyAlbum
    {
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }
        
        [JsonProperty("artists")]
        public SpotifyArtist[] Artists { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}