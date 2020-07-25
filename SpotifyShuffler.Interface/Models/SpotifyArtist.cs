using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyArtist
    {
        [JsonProperty("followers")]
        public SpotifyFollowers Followers { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}