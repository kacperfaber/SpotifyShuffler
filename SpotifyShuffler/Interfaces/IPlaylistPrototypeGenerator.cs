using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistPrototypeGenerator
    {
        Task<PlaylistPrototype> GenerateAsync(SpotifyPlaylist playlist, User user, string playlistName, string playlistDescription);
    }
}