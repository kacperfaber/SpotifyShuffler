using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class TrackPrototype
    {
        [Key]
        public Guid Id { get; set; }

        public PlaylistPrototype PlaylistPrototype { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Album { get; set; }
    }
}