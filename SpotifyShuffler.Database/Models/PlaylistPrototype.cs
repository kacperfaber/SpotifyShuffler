using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class PlaylistPrototype
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public Guid OwnerId { get; set; }

        public string OriginalPlaylistId { get; set; }

        [ForeignKey("PrototypeDataId")]
        public PlaylistPrototypeData PrototypeData { get; set; }

        public Guid PrototypeDataId { get; set; }
    }
}