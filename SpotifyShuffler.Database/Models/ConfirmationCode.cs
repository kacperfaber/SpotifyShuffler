using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class ConfirmationCode
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Code { get; set; }
    }
}