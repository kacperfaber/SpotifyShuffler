using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ICompletedPlaylistGenerator
    {
        Task<CompletedPlaylist> GenerateAsync(PlaylistPrototype prototype, SpotifyPlaylist playlist, User owner);
    }
}