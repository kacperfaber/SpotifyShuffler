using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class CreatePlaylistPayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("collaborative")]
        public bool Collaborative { get; set; }
    }
}