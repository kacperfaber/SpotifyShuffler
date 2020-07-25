using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItemsOptymalizer : IPlaylistItemsOptymalizer
    {
        public IEnumerable<PlaylistItem> Optymalize(IEnumerable<PlaylistItem> items)
        {
            foreach (PlaylistItem item in items)
            {
                IEnumerable<PlaylistItem> occurrences = items.Where(x => x.Uri == item.Uri);

                if (occurrences.Count() > 1)
                {
                    int[] positions = Array.ConvertAll(occurrences.ToArray(), x => x.Positions[0]);
                    
                    yield return new PlaylistItem
                    {
                        Uri = item.Uri,
                        Positions = positions.ToList()
                    };
                }

                else
                {
                    yield return new PlaylistItem(item.Uri, item.Positions[0]);
                }
            }
        }
    }
}