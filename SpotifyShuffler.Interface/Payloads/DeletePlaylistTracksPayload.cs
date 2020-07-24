using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class DeletePlaylistTracksPayload
    {
        [JsonProperty("tracks")]
        public List<SpotifyUri> Tracks { get; set; }
    }
}