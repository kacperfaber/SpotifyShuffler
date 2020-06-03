﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class Track
    {
        [Key]
        public Guid Id { get; set; }

        public string SpotifyId { get; set; }

        public string Name { get; set; }

        public int DurationMilliseconds { get; set; }

        public DateTime GeneratedAt { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }

        public Guid ArtistId { get; set; }
    }
}