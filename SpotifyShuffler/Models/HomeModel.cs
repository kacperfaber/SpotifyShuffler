using System.Collections.Generic;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Models
{
    public class HomeModel : LayoutModel
    {
        public SimpleSpotifyPlaylist[] SpotifyPlaylists { get; set; }
        public List<CompletedPlaylist> CompletedPlaylists { get; set; }
    }
}