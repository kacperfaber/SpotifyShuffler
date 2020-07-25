using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class Paging<TModel>
    {
        [JsonProperty("items")]
        public TModel[] Items { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}