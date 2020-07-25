using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public class PlaylistItemsGenerator : IPlaylistItemsGenerator
    {
        public IEnumerable<PlaylistItem> Generate(List<SpotifyTrack> tracks)
        {
            foreach (SpotifyTrack track in tracks)
            {
                yield return new PlaylistItem(track.Uri);
            }
        }
    }
}