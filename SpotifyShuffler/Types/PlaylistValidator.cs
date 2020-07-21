using System.Threading.Tasks;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistValidator : IPlaylistValidator
    {
        public IPlaylistSizeValidator SizeValidator;

        public PlaylistValidator(IPlaylistSizeValidator sizeValidator)
        {
            SizeValidator = sizeValidator;
        }

        public async Task<PlaylistValidationResult> ValidateAsync(SpotifyPlaylist playlist)
        {
            return await Task.Run(() =>
            {
                if (playlist == null)
                {
                    return PlaylistValidationResult.Null;
                }

                if (!SizeValidator.Validate(playlist.Tracks.Total))
                {
                    return PlaylistValidationResult.TooLarge;
                }

                return PlaylistValidationResult.Ok;
            });
        }
    }
}