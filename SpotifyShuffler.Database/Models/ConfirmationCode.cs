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

        public DateTime CreatedAt { get; set; }

        public bool IsUsed { get; set; }

        public DateTime? UsedAt { get; set; }
        public bool IsDeactivated { get; set; }

        public DateTime? DeactivatedAt { get; set; }
    }
}