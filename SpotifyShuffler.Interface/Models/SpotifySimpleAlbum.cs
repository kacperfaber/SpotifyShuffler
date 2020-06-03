using System;
using Newtonsoft.Json;
using SpotifyShuffler.Interface.Enums;

namespace SpotifyShuffler.Interface
{
    public class SpotifySimpleAlbum
    {
        [JsonProperty("album_group")]
        public AlbumGroupType AlbumGroup { get; set; }
        
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }
        
        [JsonProperty("artists")]
        public SimpleSpotifyArtist[] Artists { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}