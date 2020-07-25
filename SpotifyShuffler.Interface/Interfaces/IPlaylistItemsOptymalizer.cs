using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface IPlaylistItemsOptymalizer
    {
        IEnumerable<PlaylistItem> Optymalize(IEnumerable<PlaylistItem> items);
    }
}