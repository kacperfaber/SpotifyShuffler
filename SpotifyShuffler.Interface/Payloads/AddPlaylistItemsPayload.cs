using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class AddPlaylistItemsPayload
    {
        public AddPlaylistItemsPayload()
        {
            Uris = new List<string>();
        }

        [JsonProperty("uris")]
        public List<string> Uris { get; set; }

        [JsonProperty("position")]
        [JsonIgnore]
        public int Position { get; set; }
    }
}