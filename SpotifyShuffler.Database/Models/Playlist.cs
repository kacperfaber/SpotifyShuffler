using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class Playlist
    {
        [Key]
        public Guid Id { get; set; }

        public string SpotifyId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public Guid OwnerId { get; set; }

        [ForeignKey("SpotifyOwnerId")]
        public SpotifyUser SpotifyOwner { get; set; }

        public Guid SpotifyOwnerId { get; set; }

        public DateTime GeneratedAt { get; set; }

        [InverseProperty("Playlist")]
        public List<Track> Tracks { get; set; }
    }
}