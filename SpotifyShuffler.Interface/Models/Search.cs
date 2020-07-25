using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class Search
    {
        [JsonProperty("artists")]
        public Paging<SpotifyArtist> Artists { get; set; }

        [JsonProperty("albums")]
        public Paging<SpotifyAlbum> Albums { get; set; }

        [JsonProperty("tracks")]
        public Paging<SpotifyTrack> Tracks { get; set; }

        [JsonProperty("playlists")]
        public Paging<SpotifyPlaylist> Playlists { get; set; }
    }
}