using System;
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

        public PrimaryArtist PrimaryArtist { get; set; }

        [ForeignKey("PrimaryArtist")]
        public Guid ArtistId { get; set; }

        public PlaylistPrototype PlaylistPrototype { get; set; }

        [ForeignKey("PlaylistPrototype")]
        public Guid PlaylistPrototypeId { get; set; }

        public Playlist Playlist { get; set; }

        [ForeignKey("PlaylistId")]
        public Guid PlaylistId { get; set; }
    }
}