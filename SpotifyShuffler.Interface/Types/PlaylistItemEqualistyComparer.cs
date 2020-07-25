using System;
using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItemEqualistyComparer : IEqualityComparer<PlaylistItem>
    {
        public bool Equals(PlaylistItem x, PlaylistItem y)
        {
            return x.Uri == y.Uri;
        }

        public int GetHashCode(PlaylistItem obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}