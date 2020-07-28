using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class CompletedPlaylistGenerator : ICompletedPlaylistGenerator
    {
        public async Task<CompletedPlaylist> GenerateAsync(SpotifyPlaylist playlist, User owner)
        {
            return await Task.Run(() => new CompletedPlaylist
            {
                Id = Guid.NewGuid(),
                Owner = owner,
                GeneratedAt = DateTime.Now,
                SpotifyId = playlist.Id
            });
        }
    }
}