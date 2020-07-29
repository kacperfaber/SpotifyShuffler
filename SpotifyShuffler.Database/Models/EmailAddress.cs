using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class EmailAddress
    {
        [Key]
        public Guid Id { get; set; }

        public User User { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime? ConfirmedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
        
        public bool IsDeactivated { get; set; }
        

        public DateTime DeactivatedAt { get; set; }
    }
}