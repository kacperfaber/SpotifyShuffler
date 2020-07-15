using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class PlaylistPrototypeGenerator : IPlaylistPrototypeGenerator
    {
        public IPrototypeDataGenerator DataGenerator;
        public ITrackPrototypesGenerator PrototypesGenerator;
        public ITrackPrototypesGenerator TrackPrototypesGenerator;

        public PlaylistPrototypeGenerator(IPrototypeDataGenerator dataGenerator, ITrackPrototypesGenerator trackPrototypesGenerator)
        {
            DataGenerator = dataGenerator;
            TrackPrototypesGenerator = trackPrototypesGenerator;
        }

        public async Task<PlaylistPrototype> GenerateAsync(SpotifyPlaylist playlist, User user, string playlistName, string playlistDescription)
        {
            PlaylistPrototypeData data = await DataGenerator.GenerateAsync(playlistName, playlistDescription);

            return new PlaylistPrototype
            {
                Id = Guid.NewGuid(),
                Owner = user,
                PrototypeData = data,
                OriginalPlaylistId = playlist.Id,
                Tracks = await TrackPrototypesGenerator.GenerateAsync(playlist)
            };
        }
    }
}