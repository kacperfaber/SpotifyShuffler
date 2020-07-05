using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class SpotifyEmailValidation
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsValidated { get; set; }

        public DateTime? ValidatedAt { get; set; }

        public bool IsSuccessfullyValidated { get; set; }
        
        [ForeignKey("EmailAddressActivationId")]
        public EmailAddressActivation EmailAddressActivation { get; set; }

        public Guid EmailAddressActivationId { get; set; }
    }
}