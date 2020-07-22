using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class Operation
    {
        [Key]
        public Guid? Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime? CanceledAt { get; set; }

        public string OriginalPlaylistId { get; set; }

        public string OriginalPlaylistName { get; set; }
        
        public string OriginalPlaylistDescription { get; set; }

        [ForeignKey("PlaylistPrototypeId")]
        public PlaylistPrototype Prototype { get; set; }

        public Guid? PlaylistPrototypeId { get; set; }

        public bool IsSubmitted { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public string CreatedPlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public string PlaylistDescription { get; set; }

        public Guid OwnerId { get; set; }
    }
}