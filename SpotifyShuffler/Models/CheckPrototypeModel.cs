using System;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Models
{
    public class CheckPrototypeModel : LayoutModel
    {
        public PlaylistPrototype Prototype { get; set; }

        public string PlaylistId { get; set; }

        public Guid OperationId { get; set; }
        public bool PlaylistIsCollaborative { get; set; }
    }
}