using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class EmailAddress
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public bool IsConfirmed { get; set; }
    }
}