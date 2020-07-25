using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItemsGenerator : IPlaylistItemsGenerator
    {
        public IEnumerable<PlaylistItem> Generate(List<SpotifyTrack> tracks)
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                yield return new PlaylistItem(tracks[i].Uri, i);
            }
        }
    }
}