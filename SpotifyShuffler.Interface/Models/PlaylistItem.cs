using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItem
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("positions")]
        [JsonIgnore]
        public List<int> Positions { get; set; } = new List<int>();

        public PlaylistItem()
        {
        }

        public PlaylistItem(string uri, params int[] positions)
        {
            Positions = positions.ToList();
            Uri = uri;
        }
    }
}