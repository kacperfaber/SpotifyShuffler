using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IOperationGenerator
    {
        Operation Generate(User user, SpotifyPlaylist playlist);
    }
}