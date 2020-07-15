using System.Threading.Tasks;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistPrototypeDataGenerator
    {
        Task<PlaylistPrototypeData> GenerateAsync(string name, string description);
    }
}