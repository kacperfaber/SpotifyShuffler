using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface IPlaylistItemsGenerator
    {
        IEnumerable<PlaylistItem> Generate(List<SpotifyTrack> tracks);
    }
}