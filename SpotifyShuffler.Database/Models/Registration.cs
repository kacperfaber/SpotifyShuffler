using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; }

        public string SpotifyId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string EmailAddress { get; set; }

        public string ActivationCode { get; set; }

        public DateTime? SendedAt { get; set; }

        public DateTime? ActivatedAt { get; set; }

        public string UserName { get; set; }

        public DateTime? UserCreatedAt { get; set; }
    }
}