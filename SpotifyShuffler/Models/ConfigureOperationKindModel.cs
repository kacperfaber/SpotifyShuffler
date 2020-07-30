using System;

namespace SpotifyShuffler.Models
{
    public class ConfigureOperationKindModel : LayoutModel
    {
        public bool CreateNewPlaylist { get; set; }

        public Guid OperationId { get; set; }
    }
}