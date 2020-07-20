using System;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Models
{
    public class ExecuteSuccessfullyModel : LayoutModel
    {
        public Operation Operation { get; set; }

        public SpotifyPlaylist SpotifyPlaylist { get; set; }

        public CompletedPlaylist CompletedPlaylist { get; set; }
    }
}