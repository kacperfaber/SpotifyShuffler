using System;

namespace SpotifyShuffler.Models
{
    public class NameYourPlaylist : LayoutModel
    {
        public string PlaylistName { get; set; }

        public string PlaylistDescription { get; set; }
        public Guid OperationId { get; set; }
    }
}