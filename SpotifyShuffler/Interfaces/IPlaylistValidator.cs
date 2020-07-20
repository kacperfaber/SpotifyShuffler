using System.Threading.Tasks;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistValidator
    {
        Task<PlaylistValidationResult> ValidateAsync(SpotifyPlaylist playlist);
    }
}