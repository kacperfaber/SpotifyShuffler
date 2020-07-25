using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PlaylistTrackObject
    {
        [JsonProperty("added_at")]
        public string AddedAt { get; set; }

        [JsonProperty("added_by")]
        public PublicSpotifyUser AddedBy { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("track")]
        public SpotifyTrack Track { get; set; }
    }
}