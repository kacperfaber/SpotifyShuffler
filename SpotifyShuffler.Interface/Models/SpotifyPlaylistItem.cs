using System;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyPlaylistItem
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }
        
        [JsonProperty("track")]
        public SpotifyTrack Track { get; set; }
        
        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }
    }
}