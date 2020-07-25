using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SimpleSpotifyPlaylist
    {
        [JsonProperty("collaborative")]
        public bool Collaborative { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public PublicSpotifyUser Owner { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("type")]
        public SpotifyType Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonIgnore]
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }
}