using System;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyAlbum
    {
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }
        
        [JsonProperty("artists")]
        public SimpleSpotifyArtist[] Artists { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("label")]
        public string Label { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        
        [JsonProperty("tracks")]
        public Paging<SimpleSpotifyTrack> Tracks { get; set; }
        
        [JsonProperty("type")]
        public SpotifyType Type { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}