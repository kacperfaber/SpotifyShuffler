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
        public IPrototypesSorter Sorter;

        public PlaylistPrototypeGenerator(ITrackPrototypesGenerator trackPrototypesGenerator, IPrototypesSorter sorter)
        {
            TrackPrototypesGenerator = trackPrototypesGenerator;
            Sorter = sorter;
        }

        public async Task<PlaylistPrototype> GenerateAsync(SpotifyPlaylist playlist, Operation operation)
        {
            List<TrackPrototype> tracks = (await TrackPrototypesGenerator.GenerateAsync(playlist))
                .OrderBy(x => new Random().Next(0, 10000))
                .ToList();
            
            PlaylistPrototype proto = new PlaylistPrototype
            {
                Id = Guid.NewGuid(),
                Operation = operation,
                Tracks = tracks
            };
            
            return proto;
        }
    }
}