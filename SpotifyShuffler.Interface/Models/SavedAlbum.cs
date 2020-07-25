using System;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SavedAlbum
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("album")]
        public SpotifyAlbum Album { get; set; }
    }
}