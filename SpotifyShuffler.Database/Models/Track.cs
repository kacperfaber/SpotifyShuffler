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

        [ForeignKey("PrimaryArtistId")]
        public PrimaryArtist PrimaryArtist { get; set; }

        public Guid PrimaryArtistId { get; set; }

        [ForeignKey("PlaylistPrototypeId")]
        public PlaylistPrototype PlaylistPrototype { get; set; }

        public Guid PlaylistPrototypeId { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist Playlist { get; set; }

        public Guid PlaylistId { get; set; }
    }
}