using System;

namespace SpotifyShuffler.Models
{
    public class NameYourPlaylist
    {
        public string PlaylistName { get; set; }

        public string PlaylistDescription { get; set; }

        public string PlaylistId { get; set; }
        public Guid OperationId { get; set; }
    }
}