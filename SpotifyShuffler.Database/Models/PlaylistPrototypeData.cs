using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class PlaylistPrototypeData
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public PlaylistPrototypeData()
        {
        }

        public PlaylistPrototypeData(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}