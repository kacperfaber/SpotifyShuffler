using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PublicSpotifyUser
    {
        [JsonProperty("display_name")]
        public string Name { get; set; }

        [JsonProperty("followers")]
        public SpotifyFollowers Followers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public SpotifyType Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}