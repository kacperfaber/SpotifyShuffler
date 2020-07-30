using System;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Models
{
    public class SummaryOperationModel : LayoutModel
    {
        public Operation Operation { get; set; }
        public Guid OperationId { get; set; }
        public bool CanUseOriginalPlaylist { get; set; }
    }
}