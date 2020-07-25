using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class DeletePlaylistTracksPayload
    {
        [JsonProperty("tracks")]
        public List<PlaylistItem> Tracks { get; set; }
    }
}