using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItem
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        public PlaylistItem()
        {
        }

        public PlaylistItem(string uri)
        {
            Uri = uri;
        }
    }
}