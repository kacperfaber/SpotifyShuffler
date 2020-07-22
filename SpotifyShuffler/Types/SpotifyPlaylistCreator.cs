using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class SpotifyPlaylistCreator : ISpotifyPlaylistCreator
    {
        public async Task<SpotifyPlaylist> CreateAsync(Operation operation, User user, PlaylistService playlistService)
        {
            return await playlistService.CreatePlaylist(user.SpotifyAccountId, operation.PlaylistName, operation.PlaylistDescription, true, false);
        }
    }
}