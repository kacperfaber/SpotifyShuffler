using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class CompletedPlaylist : SimpleCompletedPlaylist
    {
        public User Owner { get; set; }
    }
}