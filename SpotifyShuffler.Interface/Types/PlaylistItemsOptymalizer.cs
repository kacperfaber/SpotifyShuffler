using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItemsOptymalizer : IPlaylistItemsOptymalizer
    {
        public IEnumerable<PlaylistItem> Optymalize(IEnumerable<PlaylistItem> items)
        {
            return items.Distinct(new PlaylistItemEqualistyComparer());
        }
    }
}