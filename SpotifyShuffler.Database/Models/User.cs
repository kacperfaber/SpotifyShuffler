using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("SpotifyUserId")]
        public SpotifyUser SpotifyUser { get; set; }

        public Guid SpotifyUserId { get; set; }

        [InverseProperty("Owner")]
        public List<PlaylistPrototype> PlaylistPrototypes { get; set; }

        [InverseProperty("Owner")]
        public List<Playlist> Playlists { get; set; }
    }
}