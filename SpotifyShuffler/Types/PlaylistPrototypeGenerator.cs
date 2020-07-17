using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistPrototypeGenerator : IPlaylistPrototypeGenerator
    {
        public ITrackPrototypesGenerator PrototypesGenerator;
        public ITrackPrototypesGenerator TrackPrototypesGenerator;

        public PlaylistPrototypeGenerator(ITrackPrototypesGenerator trackPrototypesGenerator)
        {
            TrackPrototypesGenerator = trackPrototypesGenerator;
        }

        public async Task<PlaylistPrototype> GenerateAsync(SpotifyPlaylist playlist, Operation operation)
        {
            return new PlaylistPrototype
            {
                Id = Guid.NewGuid(),
                Operation = operation,
                Tracks = await TrackPrototypesGenerator.GenerateAsync(playlist)
            };
        }
    }
}