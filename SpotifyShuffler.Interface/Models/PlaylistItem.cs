using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItem
    {
        public PlaylistItem()
        {
        }

        public PlaylistItem(string uri)
        {
            Uri = uri;
        }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}