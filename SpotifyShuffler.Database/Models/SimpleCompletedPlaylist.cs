using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class SimpleCompletedPlaylist
    {
        [Key]
        public Guid Id { get; set; }

        public string SpotifyId { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}