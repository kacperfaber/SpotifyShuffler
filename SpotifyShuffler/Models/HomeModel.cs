using System.Collections.Generic;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Models
{
    public class HomeModel : LayoutModel
    {
        public PlaylistModel[] Playlists { get; set; }
    }
}