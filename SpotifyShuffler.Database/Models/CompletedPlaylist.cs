using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class CompletedPlaylist
    {
        [Key]
        public Guid Id { get; set; }

        public string SpotifyId { get; set; }

        public User Owner { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("PlaylistPrototypeId")]
        public PlaylistPrototype PlaylistPrototype { get; set; }

        public Guid? PlaylistPrototypeId { get; set; }
    }
}