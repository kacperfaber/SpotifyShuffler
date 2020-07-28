using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ICompletedPlaylistGenerator
    {
        Task<CompletedPlaylist> GenerateAsync(SpotifyPlaylist playlist, User owner);
    }
}