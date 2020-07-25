using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Types
{
    public class CompletedPlaylistManager
    {
        public SpotifyContext SpotifyContext;

        public CompletedPlaylistManager(SpotifyContext spotifyContext)
        {
            SpotifyContext = spotifyContext;
        }

        public async Task<CompletedPlaylist> GetAsync(Guid id)
        {
            return await SpotifyContext.CompletedPlaylists
                .Where(x => x.Id == id)
                .Include(x => x.PlaylistPrototype)
                .ThenInclude(x => x.Tracks)
                .Include(x => x.Owner)
                .FirstOrDefaultAsync();
        }

        public async Task<SimpleCompletedPlaylist> GetSimpleAsync(Guid id)
        {
            return await SpotifyContext.CompletedPlaylists
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}