using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class Playlist
    {
        [Key]
        public string SpotifyId { get; set; }

        public string SpotifyOwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public Guid OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}