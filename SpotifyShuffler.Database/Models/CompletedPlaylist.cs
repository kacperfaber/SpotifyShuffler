using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class CompletedPlaylist : SimpleCompletedPlaylist
    {
        [ForeignKey("PlaylistPrototypeId")]
        public PlaylistPrototype PlaylistPrototype { get; set; }

        public Guid? PlaylistPrototypeId { get; set; }
        
        public User Owner { get; set; }
    }
}