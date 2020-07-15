using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class PlaylistPrototypeData
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PlaylistPrototypeData()
        {
        }

        public PlaylistPrototypeData(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}