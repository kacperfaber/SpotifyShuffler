using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string EmailAddress { get; set; }

        public string ActivationCode { get; set; }

        public DateTime? SendedAt { get; set; }

        public DateTime? ActivatedAt { get; set; }

        public string UserName { get; set; }

        public DateTime? UserCreatedAt { get; set; }

        [ForeignKey("SpotifyAccountId")]
        public SpotifyAccount SpotifyAccount { get; set; }

        public string SpotifyAccountId { get; set; }
    }
}