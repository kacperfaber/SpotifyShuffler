using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PlaylistPrototype> GenerateAsync(IEnumerable<SpotifyTrack> tracks, Operation operation)
        {
            List<TrackPrototype> trackPrototypes = await TrackPrototypesGenerator.GenerateAsync(tracks);
            
            PlaylistPrototype proto = new PlaylistPrototype
            {
                Id = Guid.NewGuid(),
                Tracks = trackPrototypes
            };
            
            return proto;
        }
    }
}