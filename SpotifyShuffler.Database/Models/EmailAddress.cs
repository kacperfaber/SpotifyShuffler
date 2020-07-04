using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database.Models
{
    public class EmailAddress
    {
        [Key]
        public Guid Id { get; set; }

        public string NormalizedEmail { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ValidatedAt { get; set; }
    }
}