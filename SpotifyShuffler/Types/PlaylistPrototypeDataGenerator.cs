using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistPrototypeDataGenerator : IPlaylistPrototypeDataGenerator
    {
        public async Task<PlaylistPrototypeData> GenerateAsync(string name, string description)
        {
            return await Task.Run(() => new PlaylistPrototypeData
            {
                Id = Guid.NewGuid(), 
                Description = description, 
                Name = name
            });
        }
    }
}