using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Database
{
    public class SimpleOperation
    {
        [Key]
        public Guid? Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime? CanceledAt { get; set; }

        public string OriginalPlaylistId { get; set; }

        public string OriginalPlaylistName { get; set; }
        
        public string OriginalPlaylistDescription { get; set; }
        
        public bool IsSubmitted { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public string CreatedPlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public string PlaylistDescription { get; set; }

        public Guid OwnerId { get; set; }

        public OperationKind Kind { get; set; }
    }
}