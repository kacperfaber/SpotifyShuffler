using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class TrackPrototypeGenerator : ITrackPrototypeGenerator
    {
        public IArtistLabelGenerator ArtistLabelGenerator;

        public TrackPrototypeGenerator(IArtistLabelGenerator artistLabelGenerator)
        {
            ArtistLabelGenerator = artistLabelGenerator;
        }

        public async Task<TrackPrototype> GenerateAsync(SpotifyTrack track)
        {
            return await Task.Run(() => new TrackPrototype
            {
                Album = track.Album.Name,
                Author = ArtistLabelGenerator.Generate(track.Artists),
                Id = Guid.NewGuid(),
                Name = track.Name
            });
        }
    }
}