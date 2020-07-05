using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class ActivationCode
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string EmailAddress { get; set; }

        public bool HasActivationCode { get; set; }

        public DateTime? CodeSendedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public DateTime? ValidatedAt { get; set; }

        [ForeignKey("EmailAddressActivationId")]
        public EmailAddressActivation EmailAddressActivation { get; set; }

        public Guid EmailAddressActivationId { get; set; }
    }
}