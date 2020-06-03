﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpotifyShuffler.Interface
{
    public class PrivateSpotifyUser
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        
        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("followers")]
        public SpotifyFollowers Followers { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("product")]
        [JsonConverter(typeof(StringEnumConverter))]
        public string Product { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}