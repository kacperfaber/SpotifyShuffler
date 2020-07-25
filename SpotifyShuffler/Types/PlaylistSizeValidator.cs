using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistSizeValidator : IPlaylistSizeValidator
    {
        public bool Validate(int tracks)
        {
            return tracks <= 300;
        }
    }
}