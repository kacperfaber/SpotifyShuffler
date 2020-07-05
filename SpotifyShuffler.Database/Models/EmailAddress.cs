using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class EmailAddress
    {
        [Key]
        public Guid Id { get; set; }

        public string NormalizedEmail { get; set; }

        public bool HasEmail { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? ActivatedAt { get; set; }

        [ForeignKey("ActivationId")]
        public EmailAddressActivation Activation { get; set; }

        public Guid ActivationId { get; set; }
    }
}