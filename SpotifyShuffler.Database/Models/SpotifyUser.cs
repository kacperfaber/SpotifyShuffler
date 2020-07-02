using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpotifyShuffler.Interface.Enums;

namespace SpotifyShuffler.Database.Models
{
    public class SpotifyUser
    {
        [Key]
        public Guid Id { get; set; }

        public string SpotifyId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ProductType ProductType { get; set; }

        public string Country { get; set; }
    }
}