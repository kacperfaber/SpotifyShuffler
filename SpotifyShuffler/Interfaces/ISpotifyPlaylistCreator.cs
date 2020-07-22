using System.Net;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyPlaylistCreator
    {
        Task<SpotifyPlaylist> CreateAsync(Operation operation, User user, PlaylistService playlistService);
    }
}