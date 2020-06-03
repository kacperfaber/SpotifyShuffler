using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SavedTrack
    {
        [JsonProperty("added_at")]
        public string AddedAt { get; set; }
        
        [JsonProperty("track")]
        public SpotifyTrack Track { get; set; }
    }
}