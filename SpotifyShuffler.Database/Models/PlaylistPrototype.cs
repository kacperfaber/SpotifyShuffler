using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class PlaylistPrototype
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public Guid OwnerId { get; set; }

        [InverseProperty("PlaylistPrototype")]
        public List<Track> Tracks { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
        
        public bool IsVisible { get; set; }

        public bool IsDeleted { get; set; }
    }
}