using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class SpotifyAccount
    {
        [Key]
        public string SpotifyId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RefreshedAt { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Country { get; set; }
        
        public int TotalFollowers { get; set; }
        
        public SpotifyAccountType AccountType { get; set; }
    }
}